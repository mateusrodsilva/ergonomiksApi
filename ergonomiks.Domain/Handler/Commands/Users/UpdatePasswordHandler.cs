using ergonomiks.Common.Commands;
using ergonomiks.Common.Handlers;
using ergonomiks.Domain.Commands.Authentication;
using ergonomiks.Domain.Interfaces;
using ergonomiks.Domain.Utils;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Handler.Commands.Users
{
    public class UpdatePasswordHandler : Notifiable<Notification>, IHandlerCommand<UpdatePasswordCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdatePasswordHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ICommandResult Handle(UpdatePasswordCommand command)
        {
            //Command validation
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(false, "Invalid data", command.Notifications);

            //Verify if employee is registered
            var user = _userRepository.GetById(command.Id);
            if (user == null)
            {
                return new GenericCommandResult(false, "User not found", command.Notifications);
            }

            //Valid password
            if (!Password.PasswordVerification(command.OldPassword, user.Password))
                return new GenericCommandResult(false, "Password invalid", command.Notifications);


            bool passwordVerification = Password.PasswordVerification(command.NewPassword, user.Password);

            //Verify if new password is equal to old password
            if (passwordVerification == true)
            {
                return new GenericCommandResult(false, "Password is equal to old password", command.Notifications);
            }

            //Encrypt new password
            user.Password = Password.Encrypt(command.NewPassword);
            _userRepository.UserUpdate(user);

            return new GenericCommandResult(true, "Password updated", "");
        }
    }
}
