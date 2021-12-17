using ergonomiks.Common.Handlers;
using ergonomiks.Common.Queries;
using ergonomiks.Domain.Interfaces;
using ergonomiks.Domain.Queries.Company;


namespace ergonomiks.Domain.Handler.Queries.Company
{
    public class CompanyByIdUserHandler : IHandlerQuery<CompanyListByIdQuery>
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyByIdUserHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public IQueryResult Handle(CompanyListByIdQuery query)
        {
            //Search for registered companies in the DB
            var company = _companyRepository.GetByIdUser(query.Id);

            if (company != null)
            {
                return new GenericQueryResult(true, "Company Founded", company);
            }

            return new GenericQueryResult(true, "No company found", "");
        }
    }
}
