using ergonomiks.Common.Commands;
using ergonomiks.Common.Handlers;
using ergonomiks.Domain.Commands.Company;
using ergonomiks.Domain.Interfaces;
using Flunt.Notifications;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Handler.Commands.Companies
{
    public class DeleteCompanyHandler : Notifiable<Notification>, IHandlerCommand<DeleteCompanyCommand>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAlertRepository _alertRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IUserRepository _userRepository;

        public DeleteCompanyHandler(ICompanyRepository companyRepository, IManagerRepository managerRepository, IEmployeeRepository employeeRepository, IAlertRepository alertRepository, IEquipmentRepository equipmentRepository, IUserRepository userRepository)
        {
            _companyRepository = companyRepository;
            _managerRepository = managerRepository;
            _employeeRepository = employeeRepository;
            _alertRepository = alertRepository;
            _equipmentRepository = equipmentRepository;
            _userRepository = userRepository;
        }

        public ICommandResult Handle(DeleteCompanyCommand command)
        {
            //Verify if company exists
            var company = _companyRepository.GetByIdCompany(command.Id);
            if (company == null)
            {
                return new GenericCommandResult(false, "Company not found", null);
            }

            var companyUser = _userRepository.GetById(company.IdUser);

            var employeesList = _employeeRepository.GetAllByIdCompany(command.Id);
            var managersList = _managerRepository.GetAllByIdCompany(command.Id);

            if (employeesList.Count == 0 && managersList.Count >=1 )
            {
                foreach (var manager in managersList)
                {
                    _managerRepository.Delete(manager.Id);
                    _userRepository.Delete(manager.IdUser);
                }

                _companyRepository.Delete(command.Id);
                _userRepository.Delete(companyUser.Id);
                return new GenericCommandResult(true, "Company deleted", null);
            }

            foreach (var employee in employeesList)
            {
                var equipment = _equipmentRepository.GetByIdEmployee(employee.Id);
                var alertsList = _alertRepository.GetByIdEmployee(equipment.IdEmployee);

                foreach (var item in alertsList)
                {
                    _alertRepository.Delete(item.Id);
                }

                _equipmentRepository.Delete(equipment.Id);
                _employeeRepository.Delete(employee.Id);
                _userRepository.Delete(employee.IdUser);
            }

            foreach (var manager in managersList)
            {
                _managerRepository.Delete(manager.Id);
                _userRepository.Delete(manager.IdUser);
            }

            _companyRepository.Delete(command.Id);
            _userRepository.Delete(companyUser.Id);
            return new GenericCommandResult(true, "Company deleted", null);

        }
    }
}
