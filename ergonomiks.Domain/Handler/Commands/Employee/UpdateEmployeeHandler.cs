using ergonomiks.Common.Commands;
using ergonomiks.Common.Handlers;
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
    public class UpdateEmployeeHandler : Notifiable<Notification>, IHandlerCommand<UpdateEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRepository _userRepository;

        public UpdateEmployeeHandler(IEmployeeRepository employeeRepository, IUserRepository userRepository)
        {
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
        }

        public ICommandResult Handle(UpdateEmployeeCommand command)
        {
            //Command validation
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Invalid data", command.Notifications);

            //Verify if employee is registered
            var employee = _employeeRepository.GetById(command.Id);
            if (employee == null)
            {
                return new GenericCommandResult(false, "User not found", null);
            }
            var user = _userRepository.GetById(employee.IdUser);


            // Verify if email typed is already registered
            var usersEmail = _userRepository.GetAll();
            foreach (var item in usersEmail)
            {
                if (item.Email == command.Email && item.Id != user.Id)
                {
                    return new GenericCommandResult(false, "Email already registered", null);
                }
            }

            user.Email = command.Email;
            _userRepository.UserUpdate(user);

            employee.FirstName = command.FirstName;
            employee.LastName = command.LastName;
            employee.Phone = command.Phone;
            employee.IdManager = command.IdManager;

            _employeeRepository.EmployeeUpdate(employee);
            return new GenericCommandResult(true, "Employee updated", "");
        }
    }
}
