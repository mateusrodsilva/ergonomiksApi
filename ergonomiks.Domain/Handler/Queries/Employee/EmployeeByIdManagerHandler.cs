using ergonomiks.Common.Handlers;
using ergonomiks.Common.Queries;
using ergonomiks.Domain.Interfaces;
using ergonomiks.Domain.Queries.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Handler.Queries.Employee
{
    public class EmployeeByIdManagerHandler : IHandlerQuery<EmployeesListQuery>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IManagerRepository _managerRepository;

        public EmployeeByIdManagerHandler(IEmployeeRepository employeeRepository, IManagerRepository managerRepository)
        {
            _employeeRepository = employeeRepository;
            _managerRepository = managerRepository;
        }

        public IQueryResult Handle(EmployeesListQuery query)
        {

            var managerExists = _managerRepository.GetByIdManager(query.Id);

            if (managerExists == null)
            {
                return new GenericQueryResult(true, "Manager not founded", "");
            }

            //Search for registered employees in the DB
            var employees = _employeeRepository.GetAllByIdManager(query.Id);

            if (employees.Count != 0)
            {
                return new GenericQueryResult(true, "TRUE - This manager has emplooyees", true);
            }
            else if(employees.Count == 0)
            {
                return new GenericQueryResult(true, "No employee found", false);
            }

            return new GenericQueryResult(false, "Erro", "");

        }
    }
}
