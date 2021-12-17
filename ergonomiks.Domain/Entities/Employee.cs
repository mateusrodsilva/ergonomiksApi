using ergonomiks.Common.EntityBase;
using ergonomiks.Common.Enum;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Entities
{
    public class Employee : EntityBase
    {
        public Employee()
        {

        }
        public Employee(
            string firstName,
            string lastName,            
            string phone,
            string image,
            Guid idManager,
            Guid idCompany
        )
        {
            //Validation and Notification
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotEmpty(firstName, "FirstName", "First name field cannot be empty")
                .IsNotEmpty(lastName, "LastName", "Last name field cannot be empty")                
                .IsNotEmpty(phone, "Phone", "Phone field cannot be empty")
                .AreNotEquals(idManager, Guid.Empty, "IdManager", "Manager id field cannot be empty")
                .AreNotEquals(idCompany, Guid.Empty, "IdCompany", "Company id field cannot be empty")
                );

            if (IsValid)
            {
                FirstName = firstName;
                LastName = lastName;
                Phone = phone;
                Image = image;
                IdManager = idManager;
                IdCompany = idCompany;
            }
        }



        //Atributes
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public string Phone { get; set; }
        public string Image { get; set; }
        public Guid IdUser { get; set; }
        public Guid IdManager { get; set; }
        public Guid IdCompany { get; set; }

        //Composition
        public User User { get; set; }

        public Manager Manager { get; set; }

        public Company Company { get; set; }        

        public ICollection<Equipment> Equipment { get; set; }
    }
}
