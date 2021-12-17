using ergonomiks.Common.Commands.Command;
using System;

namespace ergonomiks.Domain.Commands.User
{
    public class DeleteUserCommand : ICommand
    {
        public DeleteUserCommand()
        {
                
        }

        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }

        //Requisition body
        public Guid Id { get; set; }
        

        public void Validate()
        {
            //its not required
        }
    }
}
