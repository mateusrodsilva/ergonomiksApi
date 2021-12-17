using ergonomiks.Common.Commands.Command;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Commands.Alerts
{
    public class CreateAlertCommand : Notifiable<Notification>, ICommand
    {
        public CreateAlertCommand()
        {

        }

        public CreateAlertCommand(string title, string message, Guid idEquipment)
        {
            Title = title;
            Message = message;
            IdEquipment = idEquipment;
        }

        public string Title { get; set; }
        public string Message { get; set; }
        public Guid IdEquipment { get; set; }

        public void Validate()
        {
            //It's not required
        }
    }
}
