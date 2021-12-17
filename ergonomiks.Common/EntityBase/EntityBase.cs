using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Common.EntityBase
{
    public abstract class EntityBase : Notifiable<Notification>
    {
        public EntityBase()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }

        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
