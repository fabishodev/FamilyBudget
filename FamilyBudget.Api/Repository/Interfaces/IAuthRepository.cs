using System.Threading.Tasks;
using FamilyBudget.Entities;
using System.Data.Common;

namespace FamilyBudget.Api.Repository.Interfaces
{
    public interface IAuthRepository
    {
        DbConnection DbConnection{get; }
        Task<UserSession> Auth(Auth auth);
    }
}