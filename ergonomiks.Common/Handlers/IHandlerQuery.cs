using ergonomiks.Common.Commands;
using ergonomiks.Common.Commands.Command;
using ergonomiks.Common.Commands.Query;
using ergonomiks.Common.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Common.Handlers
{
    public interface IHandlerQuery<T> where T : IQuery
    {
        IQueryResult Handle(T query);
    }
}
