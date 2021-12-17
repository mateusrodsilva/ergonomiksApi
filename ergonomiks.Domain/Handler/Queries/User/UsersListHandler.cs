using ergonomiks.Common.Commands;
using ergonomiks.Common.Handlers;
using ergonomiks.Common.Queries;
using ergonomiks.Domain.Handler.Queries.User;
using ergonomiks.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ergonomiks.Domain.Handler.Queries.User.UsersListQuery;

namespace ergonomiks.Domain.Handler.Commands.Users
{
    public class UsersListHandler : IHandlerQuery<UsersListQuery>
    {
        private readonly IUserRepository _userRepository;

        public UsersListHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IQueryResult Handle(UsersListQuery query)
        {
            //Search for registered users in the database
            var users = _userRepository.GetAll();

            if (users.Count != 0)
            {
                var usersList = users.Select(
                    x =>
                    {
                        return new UsersListQuery()
                        {
                            Id = x.Id,
                            Email = x.Email,
                            UserType = x.UserType,
                            CreationDate = x.CreationDate
                        };
                    }
                );

                return new GenericQueryResult(true, "Users List", users);
            }

            return new GenericQueryResult(false, "No user found", users);
        }
    }
}
