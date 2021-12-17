using ergonomiks.Common.Commands.Command;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace ergonomiks.Domain.Commands.Manager
{
    public class UpdateImageManagerCommand : Notifiable<Notification>, ICommand
    {
        public UpdateImageManagerCommand()
        {

        }

        public UpdateImageManagerCommand(string image)
        {
            Image = image;
        }

        public Guid Id { get; set; }
        public string Image { get; set; }

        public void Validate()
        {
            //It's not required
        }
    }
}
