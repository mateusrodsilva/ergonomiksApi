using ergonomiks.Common.EntityBase;
using ergonomiks.Common.Enum;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;

namespace ergonomiks.Domain.Entities
{
    public class Company : EntityBase
    {
        public Company()
        {

        }
        public Company(
            string corporateName,
            string cnpj,
            string cep,
            EnCountry country
            
        )
        {
            //Validation and Notification
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotEmpty(corporateName, "CorporateName", "Company name field cannot be empty")
                .IsNotEmpty(cnpj, "Cnpj", "Cnpj field cannot be empty")
                .IsNotEmpty(cep, "Cep", "Cep field cannot be empty")
                .IsNotNull(country, "Country", "Country field cannot be empty")

                );

            if (IsValid)
            {
                CorporateName = corporateName;
                Cnpj = cnpj;
                Cep = cep;
            }
        }

        //Atributes
        public string CorporateName { get; set; }
        public string Cnpj { get; set; }
        public string Cep { get; set; }
        public EnCountry Country { get; set; }

        //Composition
        public Guid IdUser { get; set; }
        public User User { get; set; }
        public ICollection<Manager> Manager { get; set; }               
        public ICollection<Employee> Employee { get; set; }     
    }
}
