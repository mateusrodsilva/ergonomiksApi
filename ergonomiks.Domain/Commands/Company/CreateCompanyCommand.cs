using ergonomiks.Common.Commands.Command;
using ergonomiks.Common.Enum;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Commands.Company
{
    public class CreateCompanyCommand : Notifiable<Notification>, ICommand
    {
        public CreateCompanyCommand()
        {

        }

        public CreateCompanyCommand(
            string corporateName,
            string cnpj,
            string cep,
            EnCountry country
        )
        {
            if (IsValid)
            {
                CorporateName = corporateName;
                Cnpj = cnpj;
                Cep = cep;
                Country = country;
            }   
        }
        //company
        public string CorporateName { get; set; }
        public string Cnpj { get; set; }
        public string Cep { get; set; }

        //user
        public string Email { get; set; }
        public string Password { get; set; }
        public EnCountry Country { get; set; }

        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotEmpty(CorporateName, "CorporateName", "Company name field cannot be empty")
                .IsLowerThan(Cnpj, 15, "Cnpj", "Cnpj field cannot be lower than 15 chars")
                .IsNotEmpty(Cnpj, "Cnpj", "Cnpj field cannot be empty")
                .IsLowerThan(Cep, 13, "CEP", "Cep field cannot be lower than 13 chars")
                .IsNotEmpty(Cep, "CEP", "Cep field cannot be empty")
                .IsNotNull(Country,"Country", "Country field cannot be empty")
                );
        }
    }
}
