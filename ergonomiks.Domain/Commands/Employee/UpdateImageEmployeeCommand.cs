﻿using ergonomiks.Common.Commands.Command;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Commands.Employee
{
    public class UpdateImageEmployeeCommand : Notifiable<Notification>, ICommand
    {
        public UpdateImageEmployeeCommand()
        {

        }
        public UpdateImageEmployeeCommand(Guid id, string image)
        {
            Id = id;
            Image = image;
        }

        public Guid Id { get; set; }
        public string Image { get; set; }

        public void Validate()
        {
            //It's not required
        }
    }
}
