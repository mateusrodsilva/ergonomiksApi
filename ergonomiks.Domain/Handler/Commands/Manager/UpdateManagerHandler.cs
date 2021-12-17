using ergonomiks.Common.Commands;
using ergonomiks.Common.Handlers;
using ergonomiks.Common.Utils;
using ergonomiks.Domain.Commands.Manager;
using ergonomiks.Domain.Interfaces;
using Flunt.Notifications;

namespace ergonomiks.Domain.Handler.Commands.Manager
{
    public class UpdateManagerHandler : Notifiable<Notification>, IHandlerCommand<UpdateManagerCommand>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IUserRepository _userRepository;

        public UpdateManagerHandler(IManagerRepository managerRepository, IUserRepository userRepository)
        {
            _managerRepository = managerRepository;
            _userRepository = userRepository;
        }       

        public ICommandResult Handle(UpdateManagerCommand command)
        {
            //Command validation
            command.Validate();

            if (!command.IsValid)
                return new GenericCommandResult(true, "Invalid data", command.Notifications);

            //Verify if manager is registered
            var manager = _managerRepository.GetByIdManager(command.Id);

            if (manager == null)
            {
                return new GenericCommandResult(true, "User not found", null);
            }

            var user = _userRepository.GetById(manager.IdUser);

            //Verify if email typed is already registered
            var usersEmail = _userRepository.GetAll();
            foreach (var item in usersEmail)
            {
                if (item.Email == command.Email && item.Id != user.Id)
                {
                    return new GenericCommandResult(true, "Email already registered", null);
                }
            }

            user.Email = command.Email;

            manager.FirstName = command.FirstName;
            manager.LastName = command.LastName;
            manager.Phone = command.Phone;

            //Save changes on DB
            _userRepository.UserUpdate(user);
            _managerRepository.ManagerUpdate(manager);            
            return new GenericCommandResult(true, "Manager updated", "");
        }
    }
}
