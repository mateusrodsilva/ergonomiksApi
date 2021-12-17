using ergonomiks.Common.Commands.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Commands.Company
{
    public class DeleteCompanyCommand : ICommand
    {
        public DeleteCompanyCommand()
        {

        }

        public DeleteCompanyCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
        public void Validate()
        {
            //its not required
        }
    }
}
