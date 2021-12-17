using ergonomiks.Common.Commands.Command;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Commands.Equipment
{
    public class CreateEquipmentCommand : Notifiable<Notification>, ICommand
    {
        public CreateEquipmentCommand()
        {

        }

        public CreateEquipmentCommand(string temperature, string lightLevel, string moisture, Guid idEmployee)
        {
            Temperature = temperature;
            LightLevel = lightLevel;
            Moisture = moisture;
            IdEmployee = idEmployee;
        }





        //Equipment
        public string Temperature { get; set; }
        public string LightLevel { get; set; }
        public string Moisture { get; set; }
        public Guid IdEmployee { get; set; }

        public void Validate()
        {
            //It's not required
        }
    }
}
