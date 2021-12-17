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
    public class UpdateCompanyCommand : Notifiable<Notification>, ICommand
    {
        public UpdateCompanyCommand()
        {

        }

        public UpdateCompanyCommand(Guid idCompany, string corporateName, string cnpj, string cep, string email)
        {
            IdCompany = idCompany;
            CorporateName = corporateName;
            Cnpj = cnpj;
            Cep = cep;
            Email = email;
        }

        public Guid IdCompany { get; set; }
        public string CorporateName { get; set; }
        public string Cnpj { get; set; }
        public string Cep { get; set; }
        public string Email { get; set; }

        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotEmpty(CorporateName, "Corporate Name", "Corporate Name field cannot be empty")
                .IsNotEmpty(Cnpj, "Cnpj", "Cnpj field cannot be empty")
                .IsNotEmpty(Cep, "Cep", "Cep field cannot be empty")
                .IsEmail(Email, "Email", "Incorrect email format")
                );
        }
    }
}
