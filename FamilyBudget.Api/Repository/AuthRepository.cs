using System.Threading.Tasks;
using FamilyBudget.Api.Repository.Interfaces;
using FamilyBudget.Entities;
using FamilyBudget.Api.DataAccess.Interfaces;
using System.Data.Common;

namespace FamilyBudget.Api.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IData _data;

        public DbConnection DbConnection => _data.DbConnection;

        public AuthRepository(IData data)
        {
            _data = data;
        }

        public async Task<UserSession> Auth(Auth auth)
        {

            var userSession = new UserSession { UserId = 1 };

            //Si la entidad no existiera, retornamos como Id  = 0 y sin token
            //Si la entidad existe, retornamos la instancia del objeto UserSession

            return userSession;
        }
    }
}