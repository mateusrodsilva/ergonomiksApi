using ergonomiks.Common.Commands.Query;
using System;

namespace ergonomiks.Domain.Queries.Company
{
    public class CompanyListByIdQuery : IQuery
    {
        public Guid Id { get; set; }

        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
