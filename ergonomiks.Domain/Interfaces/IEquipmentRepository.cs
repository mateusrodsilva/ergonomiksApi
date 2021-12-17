using ergonomiks.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Interfaces
{
    public interface IEquipmentRepository
    {
        void Create(Equipment equipment);
        List<Equipment> GetAll();
        Equipment GetById(Guid id);
        Equipment GetByIdEmployee(Guid id);
        void Delete(Guid id);
    }
}
