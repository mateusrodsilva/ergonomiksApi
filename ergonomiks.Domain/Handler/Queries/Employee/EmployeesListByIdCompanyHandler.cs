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
    public class EmployeesListByIdCompanyHandler : IHandlerQuery<EmployeesListQuery>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesListByIdCompanyHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IQueryResult Handle(EmployeesListQuery query)
        {
            //Search for registered employees in the DB
            var employees = _employeeRepository.GetAllByIdCompany(query.Id);

            if (employees.Count != 0)
            {
                return new GenericQueryResult(true, "Employees List", employees);
            }

            return new GenericQueryResult(true, "No employee found", employees);
        }
    }
}
