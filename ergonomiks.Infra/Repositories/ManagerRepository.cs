using ergonomiks.Domain.Entities;
using ergonomiks.Domain.Interfaces;
using ergonomiks.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ergonomiks.Infra.Repositories
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly ErgonomiksContext _context;

        public ManagerRepository(ErgonomiksContext context)
        {
            _context = context;
        }

        

        /// <summary>
        /// Register a manager
        /// </summary>
        /// <param name="manager">Manager object</param>
        public void Create(Manager manager)
        {
            _context.Managers.Add(manager);
            _context.SaveChanges();
        }

        /// <summary>
        ///  Delete a manager
        /// </summary>
        /// <param name="id">Manager's id that will be deleted</param>
        public void Delete(Guid id)
        {
            Manager getManager = _context.Managers.Find(id);
            _context.Managers.Remove(getManager);
            _context.SaveChanges();
        }

        /// <summary>
        /// Search for managers in DB related with logged in company
        /// </summary>
        /// <param name="id">Company's id</param>
        /// <returns>managers list</returns>
        public List<Manager> GetAllByIdCompany(Guid id)
        {
            return _context.Managers
                .AsNoTracking()
                .Select(x => new Manager
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Phone = x.Phone,
                    Image = x.Image,
                    IdCompany = x.IdCompany,
                    IdUser = x.IdUser,

                    User = new User
                    {
                        Id = x.IdUser,
                        Email = x.User.Email,
                        UserType = x.User.UserType
                    }

                })
                .Where(x => x.IdCompany == id)
                .ToList();
        }

        public Manager GetByIdManager(Guid id)
        {
            return _context
                .Managers
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Update Manager data
        /// </summary>
        /// <param name="manager">Manager object</param>
        public void ManagerUpdate(Manager manager)
        {
            //_context.Entry(manager).State = EntityState.Modified;           
            _context.Managers.Update(manager);           
            _context.SaveChanges();
        }

        public void ManagerUpdateImage(Manager manager)
        {
            _context.Managers.Update(manager);
            _context.SaveChanges();
        }

        /// <summary>
        /// Search a manager in DB by id
        /// </summary>
        /// <param name="id">Manager's id</param>
        /// <returns>Manager object</returns>
        public Manager GetByIdUser(Guid id)
        {

            return _context
                    .Managers
                    .AsNoTracking()
                    .Select(x => new Manager
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Phone = x.Phone,
                        Image = x.Image,
                        IdCompany = x.IdCompany,
                        IdUser = x.IdUser,

                        User = new User
                        {
                            Id = x.User.Id,
                            Email = x.User.Email,
                            UserType = x.User.UserType,
                        },

                        Company = new Company
                        {
                            Id = x.IdCompany,
                            Country = x.Company.Country
                        },
                    })
                    .FirstOrDefault(x => x.IdUser == id);
        }
    }
}
