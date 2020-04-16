using System.Threading.Tasks;
using DatingApp.API.Data.Models;

namespace DatingApp.API.Services
{
    public interface IAuthService
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExists(string username);
    }
}