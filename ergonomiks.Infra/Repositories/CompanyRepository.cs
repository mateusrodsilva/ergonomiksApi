using ergonomiks.Domain.Entities;
using ergonomiks.Domain.Interfaces;
using ergonomiks.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ergonomiks.Infra.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ErgonomiksContext _context;

        public CompanyRepository(ErgonomiksContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Register a company
        /// </summary>
        /// <param name="company">Company object with company data</param>
        public void Create(Company company)
        {
            _context.Companies.Add(company);
            _context.SaveChanges();
        }

        /// <summary>
        /// Delete a company
        /// </summary>
        /// <param name="id">Company's id that will be deleted</param>
        public void Delete(Guid id)
        {
            Company getCompany = _context.Companies.Find(id);
            _context.Companies.Remove(getCompany);
            _context.SaveChanges();
        }

        /// <summary>
        /// Company list
        /// </summary>
        /// <returns>Company list</returns>
        public List<Company> GetAll()
        {
            return _context.Companies
                .AsNoTracking()
                .Select(x => 
                    new Company
                    {
                        Id = x.Id,
                        CorporateName = x.CorporateName,
                        Cnpj = x.Cnpj,
                        Cep = x.Cep,
                        Country = x.Country,
                        IdUser = x.IdUser
                    })
                .OrderBy(x => x.CreationDate)
                .ToList();
        }

        /// <summary>
        /// Search a company by cnpj
        /// </summary>
        /// <param name="cnpj">company'a cnpj</param>
        /// <returns>Company object</returns>
        public Company GetByCnpj(string cnpj)
        {
            return _context
                .Companies
                .AsNoTracking()
                .FirstOrDefault(x => x.Cnpj == cnpj);
        }

        /// <summary>
        /// Search a company by corporate name
        /// </summary>
        /// <param name="corporateName"></param>
        /// <returns>Company object</returns>
        public Company GetByCorporateName(string corporateName)
        {
            return _context
                .Companies
                .AsNoTracking()
                .FirstOrDefault(x => x.CorporateName == corporateName);
        }

        public Company GetByIdCompany(Guid id)
        {
            return _context
                .Companies
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Search a company by idUser
        /// </summary>
        /// <param name="id">Company's id</param>
        /// <returns>Company object</returns>
        public Company GetByIdUser(Guid id)
        {
            return _context
                .Companies
                .AsNoTracking()
                .Select(x => new Company
                {
                    Id = x.Id,
                    CorporateName = x.CorporateName,
                    Cnpj = x.Cnpj,
                    Cep = x.Cep,
                    Country = x.Country,
                    IdUser = x.IdUser,
                    CreationDate = x.CreationDate,

                    User = new User
                    {
                        Id = x.User.Id,
                        Email = x.User.Email,
                        UserType = x.User.UserType,
                        CreationDate = x.User.CreationDate
                    }
                }
                )
                .FirstOrDefault(x => x.IdUser == id);
        }

        /// <summary>
        /// Update a company
        /// </summary>
        /// <param name="company">Com</param>
        public void CompanyUpdate(Company company)
        {
            _context.Entry(company).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
