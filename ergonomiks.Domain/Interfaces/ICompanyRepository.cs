using ergonomiks.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Interfaces
{
    public interface ICompanyRepository
    {
        void Create(Company company);
        List<Company> GetAll();
        void CompanyUpdate(Company company);
        Company GetByIdUser(Guid id);
        Company GetByIdCompany(Guid id);
        Company GetByCnpj(string cnpj);
        Company GetByCorporateName(string corporateName);
        void Delete(Guid id);
    }
}
