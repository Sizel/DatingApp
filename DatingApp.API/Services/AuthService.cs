using System;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Data.DTOs;
using DatingApp.API.Data.Models;
using DatingApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Services
{
    public class AuthService : IAuthService
    {
        DataContext _context;
        public AuthService(DataContext context)
        {
            _context = context;
        }

       public async Task<User> Login(UserForLoginDTO userForLoginDto)
        {
            var user = await _context.Users.Include(u => u.Password).FirstOrDefaultAsync(u => u.Username == userForLoginDto.Name.ToLower());

            if (user == null)
                return null;

            if (!VerifyPassword(user.Password.PasswordHash, user.Password.PasswordSalt, userForLoginDto.Password))
                return null;

            return user;
        }

        private bool VerifyPassword(byte[] storedHash, byte[] salt, string password)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256(salt)) {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                var isHashEqual = computedHash.SequenceEqual(storedHash);
                return isHashEqual;
            }
        }
        public async Task<User> Register(UserForRegisterDTO userDto)
        {
            var userModel = new User
            {
                Username = userDto.Name.ToLower()
            };

            if (await UserExists(userModel.Username))
            {
                throw new ArgumentException("Username with this name already exists");
            }

            // 1. сгенерировать хеш и соль для пароля
            byte[] passwordHash, passwordSalt;
            GenerateHashAndSalt(userDto.Password, out passwordHash, out passwordSalt);

            userModel.Password = new Password();
            userModel.Password.PasswordHash = passwordHash;
            userModel.Password.PasswordSalt = passwordSalt;

            // 2. сохранить пользователя в базу
            await _context.Users.AddAsync(userModel);
            await _context.SaveChangesAsync();

            return userModel;
        }

        private void GenerateHashAndSalt(string password, out byte[] passwordHash, out byte[] passwordSalt) {
            using (var hmac = new System.Security.Cryptography.HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private async Task<bool> UserExists(string username)
        {
            var isExisting = await _context.Users.AnyAsync(u => u.Username == username);
            return isExisting;
        }
    }
}