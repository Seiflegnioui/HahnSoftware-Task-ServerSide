using System.Runtime.CompilerServices;
using hahn.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace hahn.Domain.Entities
{
    [Index("username", IsUnique = true)]
    [Index("email", IsUnique = true)]

    public class User
    {
        public int id { get; set; }
        public string email { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public string hashedPpassword { get; set; } = string.Empty;
        public RolesEnum role { get; set; }
        public bool AuthCompleted { get; set; } = false;
        public string? photo { get; set; }
        public DateTime joinedAt { get; set; } = DateTime.Now;

        public static User Create(
            string email,
            string username,
            string phone,
            RolesEnum role,
            string password,
            string? photo = null)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty");

            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty");

            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Phone cannot be empty");

            var user = new User
            {
                email = email,
                username = username,
                phone = phone,
                role = role,
                photo = photo ?? "default.png",
                joinedAt = DateTime.UtcNow
            };

            user.hashedPpassword = new PasswordHasher<User>().HashPassword(user, password);

            return user;
        }

        public void VerifyPassword(string password)
        {
            var result = new PasswordHasher<User>().VerifyHashedPassword(this,this.hashedPpassword, password);
            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Invalid password");
        }


    }


}