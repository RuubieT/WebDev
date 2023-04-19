using System.ComponentModel.DataAnnotations;
using System;
using WebDevAPI.Db.Dto_s.User;
using System.Diagnostics.CodeAnalysis;

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

        [AllowNull]
        public string AuthCode { get; set; }

        public GetUserDto GetUserDto()
        {
            return new()
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
            };
        }

    }
}
