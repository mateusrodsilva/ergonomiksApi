using ergonomiks.Common.Commands.Command;
using ergonomiks.Common.Enum;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Commands.User
{
    public class CreateAccountCommand : Notifiable<Notification>, ICommand
    {
        public CreateAccountCommand()
        {

        }

        public CreateAccountCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        //Requisition body
        public string Email { get; set; }
        public string Password { get; set; }

        //Flunt Validations
        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsEmail(Email, "Email", "Incorrect email format")
                .IsGreaterThan(Password, 8, "Password", "Password must be 8 characters or more")
            );
        }
    }
}
