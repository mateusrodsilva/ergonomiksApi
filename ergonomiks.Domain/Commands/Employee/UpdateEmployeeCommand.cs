using ergonomiks.Common.Commands.Command;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Commands.Employee
{
    public class UpdateEmployeeCommand : Notifiable<Notification>, ICommand
    {
        public UpdateEmployeeCommand()
        {

        }

        public UpdateEmployeeCommand(
            Guid id,
            string firstName,
            string lastName,
            string phone,
            string email
        )
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Guid IdManager { get; set; }

        public void Validate()
        {
            //It's not required
        }
    }
}
