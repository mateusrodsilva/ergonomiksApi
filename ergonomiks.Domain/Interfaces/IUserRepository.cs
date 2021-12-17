using ergonomiks.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Interfaces
{
    public interface IUserRepository
    {
        void Create(User user);
        void UserUpdate(User user);
        User GetByEmail(string email);
        User GetById(Guid id);
        List<User> GetAll();
        void Delete(Guid id);        
    }
}
