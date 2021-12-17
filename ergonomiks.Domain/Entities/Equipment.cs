using ergonomiks.Common.EntityBase;
using ergonomiks.Common.Enum;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Entities
{
    public class Equipment : EntityBase
    {
        public Equipment()
        {

        }
        public Equipment(
            string temperature,
            string lightLevel,
            string moisture,
            Guid idEmployee
            /*EnPresence presence,
            EnPosition position,
            bool confortable,
            EnUnconfortableLevel unconfortableLevel*/
        )
        {


            if (IsValid)
            {
                Temperature = temperature;
                LightLevel = lightLevel;
                Moisture = moisture;
                IdEmployee = idEmployee;
                /*Presence = presence;
                Position = position;
                Confortable = confortable;
                UnconfortableLevel = unconfortableLevel;*/
            }
        }

        public string Temperature { get; set; }
        public string LightLevel { get; set; }
        public string Moisture { get; set; }
        
        /*public EnPresence Presence { get; private set; }
        public EnPosition Position { get; private set; }
        public bool Confortable { get; private set; }
        public EnUnconfortableLevel UnconfortableLevel { get; private set; }*/

        //User
        public Guid IdEmployee { get; set; }

        public Employee Employee { get; set; }


        public ICollection<Alerts> Alerts { get; set; }
    }
}
