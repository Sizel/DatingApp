using System.Threading.Tasks;
using DatingApp.API.Data.Models;

namespace DatingApp.API.Services
{
    public interface ITokenService
    {
        Task<string> CreateJwtToken(User user, int daysValid);
    }
}