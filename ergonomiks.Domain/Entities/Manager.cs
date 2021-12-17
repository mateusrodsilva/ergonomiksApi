using ergonomiks.Common.EntityBase;
using ergonomiks.Common.Enum;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;

namespace ergonomiks.Domain.Entities
{
    public class Manager : EntityBase
    {
        public Manager()
        {
                
        }
        public Manager(
            string firstName,
            string lastName,
            string phone,
            string image,
            Guid idCompany,
            Guid idUser
        )
        {
            //Validation and Notification
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotEmpty(firstName, "FirstName", "First name field cannot be empty")
                .IsNotEmpty(lastName, "LastName", "Last name field cannot be empty")
                .IsNotEmpty(phone, "Phone", "Phone field cannot be empty")
                .IsNotEmpty(idCompany, "IdCompany", "IdCompany field cannot be empty")
                );

            if (IsValid)
            {
                FirstName = firstName;
                LastName = lastName;
                Phone = phone;
                Image = image;
                IdCompany = idCompany;
                IdUser = idUser;
            }
        }

        //Atributes
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public string Phone { get; set; }
        public string Image { get; set; }

        //Composition
        public Guid IdUser { get; set; }        
        public User User { get; set; }

        public Guid IdCompany { get; set; }
        public Company Company { get; set; }

        public ICollection<Employee> Employee { get; set; }      
    }
}
