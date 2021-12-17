using ergonomiks.Common.Commands.Command;
using ergonomiks.Common.Enum;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Commands.Employee
{
    public class CreateEmployeeCommand : Notifiable<Notification>, ICommand
    {
        public CreateEmployeeCommand()
        {

        }

        public CreateEmployeeCommand(
            string firstName,
            string lastName,
            string phone,
            string image
        )
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Image = image;

        }

        //Employee
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }

        //User
        public string Email { get; set; }
        public string Password { get; set; }


        //Company
        public Guid IdCompany { get; set; }

        //Manager
        public Guid IdManager { get; set; }

        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .AreNotEquals(IdCompany, Guid.Empty, "IdCompany", "Company id field cannot be empty")
                .AreNotEquals(IdManager, Guid.Empty, "IdManager", "Manager id field cannot be empty")
                .IsNotEmpty(FirstName, "FirstName", "First name field cannot be empty")
                .IsNotEmpty(LastName, "LastName", "Last name field cannot be empty")
                .IsNotEmpty(Phone, "Phone", "Phone field cannot be empty")
                );
        }
    }
}
