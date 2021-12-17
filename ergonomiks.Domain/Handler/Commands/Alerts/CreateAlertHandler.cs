using ergonomiks.Common.Commands;
using ergonomiks.Common.Handlers;
using ergonomiks.Common.Utils;
using ergonomiks.Domain.Commands.Alerts;
using ergonomiks.Domain.Interfaces;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Handler.Commands.Alerts
{
    public class CreateAlertHandler : Notifiable<Notification>, IHandlerCommand<CreateAlertCommand>
    {
        private readonly IAlertRepository _alertsRepository;
        private readonly IEquipmentRepository _equipmentRepository;

        public CreateAlertHandler(IAlertRepository alertsRepository, IEquipmentRepository equipmentRepository)
        {
            _alertsRepository = alertsRepository;
            _equipmentRepository = equipmentRepository;
        }

        public ICommandResult Handle(CreateAlertCommand command)
        {
            //Command validation
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult
                (
                    true,
                    "Correctly enter user data",
                    command.Notifications
                );
            }

            Entities.Alerts alert = new Entities.Alerts(command.Title, command.Message, command.IdEquipment)
            {
                Title = command.Title,
                Message = command.Message,
                IdEquipment = command.IdEquipment
            };


            //Verify if equipment is registered
            var EquipmentExists = _equipmentRepository.GetById(command.IdEquipment);


            if (EquipmentExists == null)
            {
                return new GenericCommandResult(true, "Equipment not found", "");

            }

            if (!alert.IsValid)
            {
                return new GenericCommandResult(true, "Invalid user or company data", alert.Notifications);
            }


            _alertsRepository.Create(alert);

            //Send Notification
            NotificationOnSignal.Notification(command.Title, command.Message);

            return new GenericCommandResult(true, "Alert added", "");

        }
    }
}
