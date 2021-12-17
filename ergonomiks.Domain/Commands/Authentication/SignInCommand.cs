using ergonomiks.Common.Commands.Command;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Commands.Authentication
{
    public class SignInCommand : Notifiable<Notification>, ICommand
    {

        public SignInCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }


        public string Email { get; set; }
        public string Password { get; set; }


        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
               .Requires()
               .IsEmail(Email, "Email", "Inform a valid email")
               .IsGreaterThan(Password, 8, "Password", "Password must be at least 8 characters")
           );
        }
    }
}
