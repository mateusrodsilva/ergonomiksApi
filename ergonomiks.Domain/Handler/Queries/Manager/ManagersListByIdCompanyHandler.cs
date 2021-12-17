using ergonomiks.Common.Handlers;
using ergonomiks.Common.Queries;
using ergonomiks.Domain.Interfaces;
using ergonomiks.Domain.Queries.Manager;
using System.Linq;


namespace ergonomiks.Domain.Handler.Commands.Manager
{
    public class ManagersListByIdCompanyHandler : IHandlerQuery<ManagersListQuery> 
    { 
        private readonly IManagerRepository _managerRepository;
        private readonly ICompanyRepository _companyRepository;

        public ManagersListByIdCompanyHandler(IManagerRepository managerRepository, ICompanyRepository companyRepository)
        {
            _managerRepository = managerRepository;
            _companyRepository = companyRepository;
        }

        public IQueryResult Handle(ManagersListQuery query)
        {

            var company = _companyRepository.GetByIdCompany(query.Id);

            //Search for registered managers in the DB
            var managers = _managerRepository.GetAllByIdCompany(query.Id);


            if (managers.Count != 0)
            {
                return new GenericQueryResult(true, "Managers List", managers);
            }
        
            return new GenericQueryResult(true, "No manager found", managers);
        }
    }
}
