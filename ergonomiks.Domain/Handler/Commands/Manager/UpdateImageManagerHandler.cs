using ergonomiks.Common.Commands;
using ergonomiks.Common.Handlers;
using ergonomiks.Common.Utils;
using ergonomiks.Domain.Commands.Manager;
using ergonomiks.Domain.Interfaces;
using Flunt.Notifications;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Handler.Commands.Manager
{
    public class UpdateImageManagerHandler : Notifiable<Notification>, IHandlerImageUploadCommand<UpdateImageManagerCommand>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IUserRepository _userRepository;

        public UpdateImageManagerHandler(IManagerRepository managerRepository, IUserRepository userRepository)
        {
            _managerRepository = managerRepository;
            _userRepository = userRepository;
        }

        public ICommandResult Handle(UpdateImageManagerCommand command, IFormFile file)
        {
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Invalid data", command.Notifications);

            //Verify if manager is registered
            var manager = _managerRepository.GetByIdManager(command.Id);

            if (manager == null)
            {
                return new GenericCommandResult(false, "User not found", null);
            }



            var user = _userRepository.GetById(manager.IdUser);
            var img = Image.Upload(file, user.Id);

            command.Image = img;
            manager.Image = command.Image;

            _managerRepository.ManagerUpdate(manager);

            return new GenericCommandResult(true, "Manager updated", "");
        }
    }
}
