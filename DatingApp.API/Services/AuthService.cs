using System;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Data.Models;
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
       public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == username);

            if (user == null)
                return null;

            if (!VerifyPassword(user.PasswordHash, user.PasswordSalt, password))
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
        // TO-DO: Написать тесты для этого метода
        public async Task<User> Register(User user, string password)
        {
            // 1. сгенерировать хеш и соль для пароля
            byte[] passwordHash, passwordSalt;
            GenerateHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            // 2. сохранить пользователя в базу
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void GenerateHash(string password, out byte[] passwordHash, out byte[] passwordSalt) {
            using (var hmac = new System.Security.Cryptography.HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


        public async Task<bool> UserExists(string username)
        {
            var isExisting = await _context.Users.AnyAsync(u => u.Name == username);
            return isExisting;
        }
    }
}