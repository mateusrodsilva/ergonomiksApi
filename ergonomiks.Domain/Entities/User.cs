using ergonomiks.Common.EntityBase;
using ergonomiks.Common.Enum;
using Flunt.Notifications;
using Flunt.Validations;
using System.Collections.Generic;


namespace ergonomiks.Domain.Entities
{
    public class User : EntityBase
    {
        public User()
        {

        }
        public User(string email, string password)
        {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsEmail(email, "Email", "Incorrect email format")
                .IsGreaterThan(password, 8, "Password", "Password must be 8 characters or more")

            );

            if (IsValid)
            {
                Email = email;
                Password = password;
            }

        }

        public string Email { get; set; }
        public string Password { get; set; }
        public EnUserType UserType { get; set; }
        public  Company Company { get; set; }
        public  ICollection<Manager> Manager { get; set; }                
        public ICollection<Employee> Employee  { get; set; }        
    }
}
