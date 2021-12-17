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
    public class EmployeeByIdUserHandler : IHandlerQuery<EmployeesListQuery>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeByIdUserHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IQueryResult Handle(EmployeesListQuery query)
        {
            var employee = _employeeRepository.GetByIdUser(query.Id);

            if (employee != null)
            {
                return new GenericQueryResult(true, "Employee founded", employee);
            }

            return new GenericQueryResult(true, "No employee found", employee);
        }
    }
}
