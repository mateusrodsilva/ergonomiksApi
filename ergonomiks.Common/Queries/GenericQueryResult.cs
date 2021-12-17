using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Common.Queries
{
    public class GenericQueryResult : IQueryResult
    {
        public GenericQueryResult(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public bool Success { get; private set; }
        public string Message { get; private set; }
        public Object Data { get; private set; }
    }
}
