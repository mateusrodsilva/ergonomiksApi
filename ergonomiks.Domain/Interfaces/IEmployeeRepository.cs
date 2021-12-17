using ergonomiks.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        void Create(Employee employee);
        List<Employee> GetAllByIdManager(Guid id);
        List<Employee> GetAllByIdCompany(Guid id);
        void EmployeeUpdate(Employee employee);
        Employee GetById(Guid id);
        Employee GetByIdUser(Guid id);
        void Delete(Guid id);
    }
}
