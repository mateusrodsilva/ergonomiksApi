using ergonomiks.Common.Commands.Query;
using ergonomiks.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Queries.Company
{
    public class CompaniesListQuery : IQuery
    {
        public void Validate()
        {
            //It's not required
        }

        public class CompanyListResult
        {
            //Company list columns
            public Guid Id { get; set; }
            public string CorporateName { get; set; }
            public string Cnpj { get; set; }
            public string Cep { get; set; }
            public EnCountry Country { get; set; }

            //Composition
            public Guid IdUser { get; set; }
        }
    }
}
