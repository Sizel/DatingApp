using System.Threading.Tasks;
using DatingApp.API.Data.DTOs;
using DatingApp.API.Data.Models;
using DatingApp.Data.DTOs;

namespace DatingApp.API.Services
{
    public interface IAuthService
    {
        Task<DetailedUserDTO> Register(UserForRegisterDTO userDto);
         Task<User> Login(UserForLoginDTO userForLoginDTO);
    }
}