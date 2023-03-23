using System.ComponentModel.DataAnnotations;
using System;
using WebDevAPI.Db.Dto_s.User;

namespace WebDevAPI.Db.Models
{
    public class User
    {
        [Key] 
        public Guid Id { get; set; }
        [Required] 
        public string FirstName { get; set; }
        [Required] 
        public string LastName { get; set; }
        [Required, EmailAddress] 
        public string Email { get; set; }
        [Required] 
        public string PasswordHash { get; set; }

        public User()
        {

        }

        public User(Guid id, string firstName, string lastName, string password, string email) {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = password;
        }

        public GetUserDto GetUserDto()
        {
            return new()
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                PasswordHash = PasswordHash,
            };
        }

    }
}
