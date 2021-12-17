using ergonomiks.Common.Commands.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Commands.Employee
{
    public class DeleteEmployeeCommand : ICommand
    {
        public DeleteEmployeeCommand()
        {

        }

        public DeleteEmployeeCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public void Validate()
        {
            //It's not required
        }
    }
}
