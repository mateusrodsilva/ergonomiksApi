using ergonomiks.Common.Commands.Query;
using System;


namespace ergonomiks.Domain.Queries.Manager
{
    public class ManagersListQuery : IQuery
    {
        public void Validate()
        {
            //It's not required!
        }
        
        //Managers list columns
        public Guid Id { get; set; }
    }
}
