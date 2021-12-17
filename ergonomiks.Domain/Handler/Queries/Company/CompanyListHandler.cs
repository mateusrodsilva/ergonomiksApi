
using ergonomiks.Common.Handlers;
using ergonomiks.Common.Queries;
using ergonomiks.Domain.Interfaces;
using ergonomiks.Domain.Queries.Company;
using System.Linq;
using static ergonomiks.Domain.Queries.Company.CompaniesListQuery;

namespace ergonomiks.Domain.Handler.Commands.Companies
{
    public class CompanyListHandler : IHandlerQuery<CompaniesListQuery>
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyListHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public IQueryResult Handle(CompaniesListQuery query)
        {
            //Search for registered companies in the DB
            var companies = _companyRepository.GetAll();


            if (companies != null)
            {
                return new GenericQueryResult(true, "Companies List", companies);
            }

            return new GenericQueryResult(true, "No company found", companies);

        }
    }
}
