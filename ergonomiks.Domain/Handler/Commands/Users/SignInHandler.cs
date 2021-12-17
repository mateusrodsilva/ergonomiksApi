using ergonomiks.Common.Commands;
using ergonomiks.Common.Handlers;
using ergonomiks.Common.Utils;
using ergonomiks.Domain.Commands.Authentication;
using ergonomiks.Domain.Interfaces;
using ergonomiks.Domain.Utils;
using Flunt.Notifications;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Handler.Commands.Users
{
    public class SignInHandler : Notifiable<Notification>, IHandlerCommand<SignInCommand>
    {
        private readonly IUserRepository _userRepository;

        public SignInHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        
        public ICommandResult Handle(SignInCommand command)
        {
            //Command validation
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Email or password invalids", command.Notifications);

            //Verify if email is already registered
            var userRegistered = _userRepository.GetByEmail(command.Email);

            if (userRegistered == null)
                return new GenericCommandResult(false, "Email invalid", command.Notifications);

            //Password Validation
            if (!Password.PasswordVerification(command.Password, userRegistered.Password))
                return new GenericCommandResult(false, "Password invalid", command.Notifications);

            return new GenericCommandResult(true, "User logged", userRegistered);
        }
    }
}
