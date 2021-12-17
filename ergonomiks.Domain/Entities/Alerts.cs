using ergonomiks.Common.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Entities
{
    public class Alerts : EntityBase
    {
        public Alerts()
        {

        }

        public Alerts(string title, string message, Guid idEquipment)
        {
            Title = title;
            Message = message;
            IdEquipment = idEquipment;
        }

        public string Title { get; set; }
        public string Message { get; set; }
        public Guid IdEquipment { get; set; }


        public Equipment Equipment { get; set; }
    }
}
