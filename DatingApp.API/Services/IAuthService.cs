using System.Threading.Tasks;
using DatingApp.API.Data.DTOs;
using DatingApp.API.Data.Models;

namespace DatingApp.API.Services
{
    public interface IAuthService
    {
         Task<User> Register(UserForRegisterDTO userForRegisterDTO);
         Task<User> Login(UserForLoginDTO userForLoginDTO);
    }
}