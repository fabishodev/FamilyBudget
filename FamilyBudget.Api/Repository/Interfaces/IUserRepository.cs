using System.Threading.Tasks;
using System.Collections.Generic;
using FamilyBudget.Entities;
using System.Data.Common;

namespace FamilyBudget.Api.Repository.Interfaces
{
    public interface IUserRepository
    {
        DbConnection DbConnection{get; }
         Task<IEnumerable<User>> GetAll();
         Task<User> GetById(int id);
         Task<User> Add(User user);
         Task<User> Update(User user);
         Task<bool> Delete(int id);
    }
}