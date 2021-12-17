using ergonomiks.Common.Commands;
using ergonomiks.Common.Handlers;
using ergonomiks.Domain.Commands.Equipment;
using ergonomiks.Domain.Interfaces;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Handler.Commands.Equipment
{
    public class CreateEquipmentHandler : Notifiable<Notification>, IHandlerCommand<CreateEquipmentCommand>
    {
        private readonly IEquipmentRepository _equipmentRepository;

        public CreateEquipmentHandler(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }

        public ICommandResult Handle(CreateEquipmentCommand command)
        {
            //Command validation
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult
                (
                    false,
                    "correctly enter manager data",
                    command.Notifications
                );
            }

            Entities.Equipment equipment = new Entities.Equipment(command.Temperature, command.LightLevel, command.Moisture, command.IdEmployee);


            if (!equipment.IsValid)
                return new GenericCommandResult(false, "Invalid equipment data", equipment.Notifications);

            _equipmentRepository.Create(equipment);

            return new GenericCommandResult(true, "Equipment added", "");
        }
    }
}
