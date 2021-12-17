using ergonomiks.Common.Commands;
using ergonomiks.Common.Handlers;
using ergonomiks.Common.Queries;
using ergonomiks.Domain.Interfaces;
using ergonomiks.Domain.Queries.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Handler.Commands.Employee
{
    public class EmployeesListByIdManagerHandler : IHandlerQuery<EmployeesListQuery>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesListByIdManagerHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IQueryResult Handle(EmployeesListQuery query)
        {
            //Search for registered employees in the DB
            var employees = _employeeRepository.GetAllByIdManager(query.Id);

            if (employees.Count != 0)
            {
                return new GenericQueryResult(true, "Employees List", employees);
            }

            return new GenericQueryResult(true, "No employee found", employees);
        }
    }
}
