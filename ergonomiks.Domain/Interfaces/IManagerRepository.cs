using ergonomiks.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Interfaces
{
    public interface IManagerRepository
    {
        void Create(Manager manager);
        List<Manager> GetAllByIdCompany(Guid id);
        void ManagerUpdate(Manager manager);
        void ManagerUpdateImage(Manager manager);
        Manager GetByIdUser(Guid id);
        Manager GetByIdManager(Guid id);
        void Delete(Guid id);
     
    }
}
