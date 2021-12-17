using ergonomiks.Common.Commands;
using ergonomiks.Common.Enum;
using ergonomiks.Common.Handlers;
using ergonomiks.Common.Services;
using ergonomiks.Common.Utils;
using ergonomiks.Domain.Commands.Employee;
using ergonomiks.Domain.Interfaces;
using ergonomiks.Domain.Utils;
using Flunt.Notifications;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Handler.Commands.Employee
{
    public class CreateEmployeeHandler : Notifiable<Notification>, IHandlerImageUploadCommand<CreateEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMailService _mailService;

        public CreateEmployeeHandler(IEmployeeRepository employeeRepository, IUserRepository userRepository, IMailService mailService)
        {
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
            _mailService = mailService;
        }


        public ICommandResult Handle(CreateEmployeeCommand command, IFormFile file)
        {
            //Command validation
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult
                (
                    false,
                    "correctly enter employee data",
                    command.Notifications
                );
            }

            var emailRegistered = _userRepository.GetByEmail(command.Email);

            if (emailRegistered != null)
            {
                return new GenericCommandResult(false, "Email already registered", command.Notifications);
            }


            Entities.User user = new Entities.User(
               command.Email,
               command.Password
           )
            {
                Email = command.Email,
                //Encrypt password
                Password = Password.Encrypt(command.Password),
                UserType = EnUserType.employee
            };

            var img = Image.Upload(file, user.Id);

            if (img == null)
            {
                return new GenericCommandResult
                (
                    false,
                    "invalid file type",
                    command.Notifications
                );
            }

            command.Image = img;

            Entities.Employee employee = new Entities.Employee(
               command.FirstName,
               command.LastName,
               command.Phone,
               command.Image,
               command.IdManager,
               command.IdCompany
           )
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Phone = command.Phone,
                Image = command.Image,
                IdCompany = command.IdCompany,
                IdManager = command.IdManager,
                IdUser = user.Id
            };

            //Verify if user data is valid
            if (!user.IsValid)
                return new GenericCommandResult(false, "Invalid user data", user.Notifications);
            //Verify if employee data is valid
            if (!employee.IsValid)
                return new GenericCommandResult(false, "Invalid employee data", employee.Notifications);

            _mailService.SendAlertEmail(command.Email, command.Password);
            //Save user and emplyee on DB
            _userRepository.Create(user);
            _employeeRepository.Create(employee);
            return new GenericCommandResult(true, "Employee added", "");

        }
    }
}
