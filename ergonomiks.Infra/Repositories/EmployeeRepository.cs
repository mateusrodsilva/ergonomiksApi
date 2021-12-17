using ergonomiks.Domain.Entities;
using ergonomiks.Domain.Interfaces;
using ergonomiks.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Infra.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ErgonomiksContext _context;

        public EmployeeRepository(ErgonomiksContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Register a employee
        /// </summary>
        /// <param name="employee">Employee object</param>
        public void Create(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        /// <summary>
        /// Delete a employee
        /// </summary>
        /// <param name="id">Emplyee's id that will be deleted</param>
        public void Delete(Guid id)
        {
            Employee getEmployee = _context.Employees.Find(id);
            _context.Employees.Remove(getEmployee);
            _context.SaveChanges();
        }

        /// <summary>
        /// Employee List
        /// </summary>
        /// <returns>Employee List</returns>
        public List<Employee> GetAllByIdManager(Guid id)
        {
            return _context
                .Employees
                .AsNoTracking()
                .Select(
                x => new Employee
                {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Phone = x.Phone,
                        Image = x.Image,
                        CreationDate = x.CreationDate,
                        IdUser = x.IdUser,

                        User = new User
                        {
                            Id = x.IdUser,
                            Email = x.User.Email,
                            UserType = x.User.UserType
                        },

                        IdCompany = x.IdCompany,
                        IdManager = x.IdManager
                })
                .Where(x => x.IdManager == id)
                .OrderBy(x => x.CreationDate)
                .ToList();
        }

        /// <summary>
        /// Update employee
        /// </summary>
        /// <param name="employee">Emplyee object</param>
        public void EmployeeUpdate(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            _context.SaveChanges();
        }

        /// <summary>
        /// Search a employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Employee IEmployeeRepository.GetById(Guid id)
        {
            return _context
                .Employees
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Employee> GetAllByIdCompany(Guid id)
        {
            return _context
                .Employees
                .AsNoTracking()
                .Select(
                x => new Employee
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Phone = x.Phone,
                        Image = x.Image,
                        CreationDate = x.CreationDate,
                        IdUser = x.IdUser,

                        User = new User
                        {
                            Id = x.IdUser,
                            Email = x.User.Email,
                            UserType = x.User.UserType
                        },

                        IdCompany = x.IdCompany,
                        IdManager = x.IdManager,

                        Manager = new Manager
                        {
                            Id = x.IdManager,
                            FirstName = x.Manager.FirstName,
                            LastName = x.Manager.LastName,
                        },
                    })
                .Where(x => x.IdCompany == id)
                .OrderBy(x => x.CreationDate)
                .ToList();
        }

        public Employee GetByIdUser(Guid id)
        {
            return _context
                .Employees
                .AsNoTracking()
                .Select(x => new Employee
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Phone = x.Phone,
                    Image = x.Image,
                    IdCompany = x.IdCompany,
                    IdUser = x.IdUser,
                    IdManager = x.IdManager,

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
                    }
                })
                .FirstOrDefault(x => x.IdUser == id);
        }
    }
}
