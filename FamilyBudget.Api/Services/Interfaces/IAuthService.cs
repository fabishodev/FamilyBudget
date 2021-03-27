using System.Threading.Tasks;
using FamilyBudget.Entities;

namespace FamilyBudget.Api.Services.Interfaces
{
    public interface IAuthService
    {
       Task<UserSession> Auth(Auth auth);    
       string GenerateJwtToken(UserSession session);    
       Task<User> GetUserBydId(int id);  
    }
}