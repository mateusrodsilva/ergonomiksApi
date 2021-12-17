using ergonomiks.Common.Commands.Command;
using Flunt.Notifications;
using Flunt.Validations;
using System;


namespace ergonomiks.Domain.Commands.Manager
{
    public class UpdateManagerCommand : Notifiable<Notification>, ICommand
    {
        public UpdateManagerCommand()
        {

        }

        public UpdateManagerCommand(
            string firstName,
            string lastName,
            string phone,
            string email
        )
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
        }

        //Manager
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }

        //User
        public string Email { get; set; }

        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsEmail(Email, "Email", "Incorrect email format")
                );
        }
    }
}
