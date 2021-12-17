using ergonomiks.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Interfaces
{
    public interface IAlertRepository
    {
        void Create(Alerts alert);
        List<Alerts> GetByIdEmployee(Guid idEmployee);
        void Delete(Guid id);
    }
}
