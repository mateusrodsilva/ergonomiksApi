using ergonomiks.Common.Commands;
using ergonomiks.Common.Handlers;
using ergonomiks.Domain.Commands.User;
using ergonomiks.Domain.Interfaces;
using ergonomiks.Domain.Utils;
using Flunt.Notifications;
using ergonomiks.Common.Enum;
using ergonomiks.Common.Utils;

namespace ergonomiks.Domain.Handler.Commands.User
{
    public class CreateUserHandler : Notifiable<Notification>, IHandlerCommand<CreateAccountCommand>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ICommandResult Handle(CreateAccountCommand command)
        {
            //Command Validation
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult
                (
                    false,
                    "Correctly enter user data",
                    command.Notifications
                );
            }


            //Verify if user already exists
            var userExist = _userRepository.GetByEmail(command.Email);
            if (userExist != null)
            {
                return new GenericCommandResult(false, "Email already registered", "Inform another email");
            }

            //Encrypting Password
            command.Password = Password.Encrypt(command.Password);


            Entities.User u1 = new Entities.User(
                    command.Email,
                    command.Password)
            {
                Email = command.Email,
                Password = command.Password,
                UserType = EnUserType.admin
            };
            

            //Verify if user data is valid
            if (!u1.IsValid)
                return new GenericCommandResult(false, "Invalid user data", u1.Notifications);
            
            //Save user on DB
            _userRepository.Create(u1);

            //Send Notification
            NotificationOnSignal.Notification("User Create","User created successfully ");           

            return new GenericCommandResult(true, "User created", "");
        }
    }
}
