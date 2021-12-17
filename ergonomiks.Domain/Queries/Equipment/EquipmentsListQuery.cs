using ergonomiks.Common.Commands.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Queries.Equipment
{
    public class EquipmentsListQuery : IQuery
    {
        public void Validate()
        {
            //It's not required!
        }

        public Guid Id { get; set; }
        public string Temperature { get; set; }
        public string LightLevel { get; set; }
        public string Noise { get; set; }
        public string Moisture { get; set; }
        //User
        public Guid IdEmployee { get; set; }

    }
}
