using ergonomiks.Common.Commands;
using ergonomiks.Common.Enum;
using ergonomiks.Common.Handlers;
using ergonomiks.Common.Services;
using ergonomiks.Common.Utils;
using ergonomiks.Domain.Commands.Manager;
using ergonomiks.Domain.Interfaces;
using ergonomiks.Domain.Utils;
using Flunt.Notifications;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Handler.Commands.Manager
{
    public class CreateManagerHandler : Notifiable<Notification>, IHandlerImageUploadCommand<CreateManagerCommand>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMailService _mailService;

        public CreateManagerHandler(IManagerRepository managerRepository, IUserRepository userRepository, IMailService mailService)
        {
            _managerRepository = managerRepository;
            _userRepository = userRepository;
            _mailService = mailService;
        }


        public ICommandResult Handle(CreateManagerCommand command, IFormFile file)
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

            //Search if email is already registered
            var emailRegistered = _userRepository.GetByEmail(command.Email);

            if (emailRegistered != null)
            {
                return new GenericCommandResult(false, "Email already registered", command.Notifications);
            }

            Entities.User user = new Entities.User(
               command.Email,
               command.Password
           )
            {
                Email = command.Email,
                //Encrypt password
                Password = Password.Encrypt(command.Password),
                UserType = EnUserType.manager
            };


            var img = Image.Upload(file, user.Id);

            if (img == null)
            {
                return new GenericCommandResult
                (
                    false,
                    "invalid file type",
                    command.Notifications
                );
            }

            command.Image = img;




            Entities.Manager manager = new Entities.Manager(
                command.FirstName,
                command.LastName,
                command.Phone,
                command.Image,
                command.IdCompany,
                user.Id
            )
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Phone = command.Phone,
                Image = command.Image,
                IdCompany = command.IdCompany,
                IdUser = user.Id
            };
            
            //Verify if user data is valid
            if (!user.IsValid)
                return new GenericCommandResult(false, "Invalid user data", user.Notifications);
            
            //Verify if manager data is valid
            if (!manager.IsValid)
                return new GenericCommandResult(false, "Invalid manager data", manager.Notifications);

            _mailService.SendAlertEmail(command.Email, command.Password);
            //Save user and manager on DB
            _userRepository.Create(user);
            _managerRepository.Create(manager);

            return new GenericCommandResult(true, "Manager added", "");
        }
    }
}
