using ergonomiks.Common.Commands;
using ergonomiks.Common.Handlers;
using ergonomiks.Domain.Commands.Company;
using ergonomiks.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Handler.Commands.Companies
{
    public class UpdateCompanyHandler : IHandlerCommand<UpdateCompanyCommand>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;

        public UpdateCompanyHandler(ICompanyRepository companyRepository, IUserRepository userRepository)
        {
            _companyRepository = companyRepository;
            _userRepository = userRepository;
        }

        public ICommandResult Handle(UpdateCompanyCommand command)
        {
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Invalid data", command.Notifications);
            
            //Verify if company is registered
            var company = _companyRepository.GetByIdCompany(command.IdCompany);
            if (company == null)
            {
                return new GenericCommandResult(false, "User not found", null);
            }

            var user = _userRepository.GetById(company.IdUser);

            // Verify if email typed is already registered
            var usersEmail = _userRepository.GetAll();
            foreach (var item in usersEmail)
            {
                if (item.Email == command.Email && item.Id != user.Id)
                {
                    return new GenericCommandResult(false, "Email already registered", null);
                }
            }

            //Verify if Company's Corporate Name is already registered 
            var CompanyData = _companyRepository.GetAll();
            foreach (var item in CompanyData)
            {
                if (item.CorporateName == command.CorporateName && item.IdUser != user.Id)
                {
                    return new GenericCommandResult(false, "Corporate Name already registered", null);
                }

                if (item.Cnpj == command.Cnpj && item.IdUser != user.Id)
                {
                    return new GenericCommandResult(false, "CNPJ already registered", null);

                }
            }

            user.Email = command.Email;

            company.CorporateName = command.CorporateName;
            company.Cnpj = command.Cnpj;
            company.Cep = command.Cep;

            _companyRepository.CompanyUpdate(company);
            _userRepository.UserUpdate(user);

            return new GenericCommandResult(true, "Company updated", "");

        }
    }
}
