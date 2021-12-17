using ergonomiks.Domain.Entities;
using ergonomiks.Domain.Handler.Queries.User;
using ergonomiks.Domain.Interfaces;
using ergonomiks.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ergonomiks.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ErgonomiksContext _context;

        public UserRepository(ErgonomiksContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Register a user
        /// </summary>
        /// <param name="user">User object with user data</param>
        public void Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id">User id that will br deleted</param>
        public void Delete(Guid id)
        {
            User getUser = _context.Users.Find(id);
            _context.Users.Remove(getUser);
            _context.SaveChanges();            
        }

        /// <summary>
        /// User list
        /// </summary>
        /// <returns>User list</returns>
        public List<User> GetAll()
        {
            return _context.Users
               .AsNoTracking()
               .Select(
                    x => new User
                    {
                        Id = x.Id,
                        Email = x.Email,
                        UserType = x.UserType,
                        CreationDate = x.CreationDate
                    })
               .OrderBy(x => x.CreationDate)
               .ToList();
        }

        /// <summary>
        /// Search a user by email
        /// </summary>
        /// <param name="email">User's email</param>
        /// <returns>User object</returns>
        public User GetByEmail(string email)
        {
            return _context
                .Users
                .AsNoTracking()
                .FirstOrDefault(x => x.Email == email);
        }

        /// <summary>
        /// Search a user by id
        /// </summary>
        /// <param name="id">User's Id</param>
        /// <returns>User object</returns>
        public User GetById(Guid id)
        {
            return _context
                .Users
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Update a user data
        /// </summary>
        /// <param name="user">User object</param>
        public void UserUpdate(User user)
        {
            //_context.Users.Update(user);
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();            
        }

        /// <summary>
        /// Update user password
        /// </summary>
        /// <param name="user">User object</param>
        public void PasswordUpdate(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
