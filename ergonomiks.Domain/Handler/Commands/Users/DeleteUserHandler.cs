using ergonomiks.Common.Commands;
using ergonomiks.Common.Handlers;
using ergonomiks.Domain.Commands.User;
using ergonomiks.Domain.Interfaces;
using Flunt.Notifications;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Handler.Commands.Users
{
    public class DeleteUserHandler : Notifiable<Notification>, IHandlerCommand<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ICommandResult Handle(DeleteUserCommand command)
        {
            //Verify if user exists
            var user = _userRepository.GetById(command.Id);


            if (user != null)
            {
                _userRepository.Delete(command.Id);
                return new GenericCommandResult(true, "User deleted", null);
            }

            return new GenericCommandResult(false, "User not found", null);

        }
    }
}
