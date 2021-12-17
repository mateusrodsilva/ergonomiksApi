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
    public class UpdatePasswordCommand : Notifiable<Notification>, ICommand
    {
        public UpdatePasswordCommand(Guid id, string oldPassword, string newPassword)
        {
            Id = id;
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }

        public Guid Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
               .Requires()
               .IsGreaterThan(NewPassword, 8, "Password", "Password must be at least 8 characters")
           );
        }
    }
}
