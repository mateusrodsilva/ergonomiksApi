using ergonomiks.Common.Handlers;
using ergonomiks.Common.Queries;
using ergonomiks.Domain.Interfaces;
using ergonomiks.Domain.Queries.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Handler.Queries.Manager
{
    public class ManagerByIdUserHandler : IHandlerQuery<ManagersListQuery>
    {
        private readonly IManagerRepository _managerRepository;

        public ManagerByIdUserHandler(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        public IQueryResult Handle(ManagersListQuery query)
        {
            //Search for registered companies in the DB
            var manager = _managerRepository.GetByIdUser(query.Id);

            if (manager != null)
            {
                return new GenericQueryResult(true, "Manager Founded", manager);
            }

            return new GenericQueryResult(false, "No manager found", manager);
        }
    }
}
