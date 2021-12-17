using ergonomiks.Common.Commands.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Queries.Employee
{
    public class EmployeesListQuery : IQuery
    {
        public void Validate()
        {
            //It's not required!
        }

        //Employees list columns
        public Guid Id { get; set; }
    }
}
