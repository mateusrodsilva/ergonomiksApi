using ergonomiks.Common.Commands;
using ergonomiks.Common.Handlers;
using ergonomiks.Common.Utils;
using ergonomiks.Domain.Commands.Employee;
using ergonomiks.Domain.Interfaces;
using Flunt.Notifications;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Handler.Commands.Employee
{
    public class UpdateImageEmployeeHandler : Notifiable<Notification>, IHandlerImageUploadCommand<UpdateImageEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRepository _userRepository;

        public UpdateImageEmployeeHandler(IEmployeeRepository employeeRepository, IUserRepository userRepository)
        {
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
        }

        public ICommandResult Handle(UpdateImageEmployeeCommand command, IFormFile file)
        {
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Invalid data", command.Notifications);

            //Verify if manager is registered
            var employee = _employeeRepository.GetById(command.Id); //IdEmployee

            if (employee == null)
            {
                return new GenericCommandResult(true, "User not found", null);
            }

            var user = _userRepository.GetById(employee.IdUser);
            var img = Image.Upload(file, user.Id);

            command.Image = img;
            employee.Image = command.Image;

            _employeeRepository.EmployeeUpdate(employee);

            return new GenericCommandResult(true, "Manager updated", "");
        }
    }
}
