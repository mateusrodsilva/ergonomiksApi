using ergonomiks.Common.Commands.Command;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Commands.Manager
{
    public class DeleteManagerCommand : Notifiable<Notification>, ICommand
    {
        public DeleteManagerCommand()
        {

        }

        public DeleteManagerCommand(Guid id, Guid newIdManager)
        {
            Id = id;
            NewIdManager = newIdManager;
        }

        public Guid Id { get; set; }
        public Guid NewIdManager { get; set; }

        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotEmpty(Id, "IdManager", "IdManager field cannot be empty")
                .IsNotEmpty(NewIdManager, "NewIdManager", "NewIdManager field cannot be empty")

                );
        }

    }
}
