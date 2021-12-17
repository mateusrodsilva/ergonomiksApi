using ergonomiks.Common.Commands.Query;
using ergonomiks.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Handler.Queries.User
{
    public class UsersListQuery : IQuery
    {
        public void Validate()
        {
            //It's not required
        }

        //User list Columns

        public Guid Id { get; set; }
        public string Email { get; set; }
        public EnUserType UserType { get; set; }
        public DateTime CreationDate { get; set; }

  
    }
}
