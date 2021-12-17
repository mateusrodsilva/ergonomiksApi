using ergonomiks.Common.Commands;
using ergonomiks.Common.Handlers;
using ergonomiks.Common.Queries;
using ergonomiks.Domain.Commands.Equipment;
using ergonomiks.Domain.Interfaces;
using ergonomiks.Domain.Queries.Equipment;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Handler.Commands.Equipment
{
    public class ListEquipmentHandler : IHandlerQuery<EquipmentsListQuery>
    {
        private readonly IEquipmentRepository _equipmentRepository;

        public ListEquipmentHandler(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }

        public IQueryResult Handle(EquipmentsListQuery query)
        {
            //Search for equipments in database
            var equipments = _equipmentRepository.GetAll();

            if (equipments != null)
            {
                var equipmentsList = equipments.Select(
                    x =>
                    {
                        return new EquipmentsListQuery()
                        {
                            Id = x.Id,
                            Temperature = x.Temperature,
                            LightLevel = x.LightLevel,
                            //Noise = x.Noise,
                            Moisture = x.Moisture,
                            IdEmployee = x.IdEmployee
                        };
                    });
                return new GenericQueryResult(true, "Equipments List", equipmentsList);
            }

            return new GenericQueryResult(false, "No equipment found", "");
        }
    }
}
