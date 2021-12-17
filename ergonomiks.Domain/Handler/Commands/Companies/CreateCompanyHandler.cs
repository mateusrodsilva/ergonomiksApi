using ergonomiks.Common.Commands;
using ergonomiks.Common.Enum;
using ergonomiks.Common.Handlers;
using ergonomiks.Common.Services;
using ergonomiks.Domain.Commands.Company;
using ergonomiks.Domain.Interfaces;
using ergonomiks.Domain.Utils;
using Flunt.Notifications;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Handler.Commands.Companies
{
    public class CreateCompanyHandler : Notifiable<Notification>, IHandlerCommand<CreateCompanyCommand>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMailService _mailService;

        public CreateCompanyHandler(ICompanyRepository companyRepository, IUserRepository userRepository, IMailService mailService)
        {
            _companyRepository = companyRepository;
            _userRepository = userRepository;
            _mailService = mailService;
        }

        public ICommandResult Handle(CreateCompanyCommand command)
        {
            //Command validation
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult
                (
                    true,
                    "Correctly enter user data",
                    command.Notifications
                );
            }


            Entities.User user = new Entities.User(
                command.Email,
                command.Password    
            )
            {
                Email = command.Email,
                //Encrypting Password
                Password = Password.Encrypt(command.Password), 
                UserType = EnUserType.company
            };



            Entities.Company company = new Entities.Company(
                command.CorporateName,
                command.Cnpj,
                command.Cep,
                command.Country
            )
            {
                IdUser = user.Id,
                CorporateName = command.CorporateName,
                Cnpj = command.Cnpj,
                Cep = command.Cep,
                Country = command.Country

            };

            //Verify if Company's Corporate Name is already registered 
            var CorporateNameExist = _companyRepository.GetByCorporateName(command.CorporateName);
            
            if (CorporateNameExist != null)
            {
                return new GenericCommandResult(true, "Corporate Name already registered", "Inform another Corporate Name");
            }

            //Verify if Company's CNPJ is already registered 
            var cnpjExist = _companyRepository.GetByCnpj(command.Cnpj);
            
            if (cnpjExist != null)
            {
                return new GenericCommandResult(true, "CNPJ already registered", "Inform another CNPJ");
            }


            if (!company.IsValid)
            {
                return new GenericCommandResult(true, "Invalid company data", company.Notifications);
            }

            //Verify if user or company data is valid
            if (!user.IsValid ) 
            {
                return new GenericCommandResult(true, "Invalid user or company data", user.Notifications);
            }

            _mailService.SendAlertEmail(command.Email,command.Password);
            //Save user and company in DB
            _userRepository.Create(user);
            _companyRepository.Create(company);



            return new GenericCommandResult(true, "Company added", "");

        }
    }
}
