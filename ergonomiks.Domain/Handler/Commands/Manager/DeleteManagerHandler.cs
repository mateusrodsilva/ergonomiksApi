using ergonomiks.Common.Commands;
using ergonomiks.Common.Handlers;
using ergonomiks.Domain.Commands.Manager;
using ergonomiks.Domain.Interfaces;
using Flunt.Notifications;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Handler.Commands.Manager
{
    public class DeleteManagerHandler : Notifiable<Notification>, IHandlerCommand<DeleteManagerCommand>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRepository _userRepository;

        public DeleteManagerHandler(IManagerRepository managerRepository, IEmployeeRepository employeeRepository, IUserRepository userRepository)
        {
            _managerRepository = managerRepository;
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
        }

        public ICommandResult Handle(DeleteManagerCommand command)
        {
            //Verify if manager exists
            var manager = _managerRepository.GetByIdManager(command.Id);
            if (manager == null)
            {
                return new GenericCommandResult(true, "Manager not found", null);
            }

            var managerUser = _userRepository.GetById(manager.IdUser);
            var employeesTeam = _employeeRepository.GetAllByIdManager(command.Id);

            if (employeesTeam.Count == 0)
            {
                _managerRepository.Delete(command.Id);
                _userRepository.Delete(managerUser.Id);
                return new GenericCommandResult(true, "Manager deleted, No employees related", null);
            }

            foreach (var item in employeesTeam)
            {
                item.IdManager = command.NewIdManager;
                _employeeRepository.EmployeeUpdate(item);
            }

            _managerRepository.Delete(command.Id);
            _userRepository.Delete(managerUser.Id);
            return new GenericCommandResult(true, "Manager deleted", null);
        }
    }
}
