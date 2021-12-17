using ergonomiks.Common.Commands.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Queries.Alert
{
    public class AlertByIdEmployeeQuery : IQuery
    {
        public Guid Id { get; set; } // IdEmployee

        public void Validate()
        {
            //It's not required
        }
    }
}
