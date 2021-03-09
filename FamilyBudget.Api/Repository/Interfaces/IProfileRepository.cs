using System.Threading.Tasks;
using System.Collections.Generic;
using FamilyBudget.Entities;
using System.Data.Common;

namespace FamilyBudget.Api.Repository.Interfaces
{
    public interface IProfileRepository 
    {

        DbConnection DbConnection{get; }        
        Task<IEnumerable<Profile>> GetAll();
        Task<Profile> GetById(int id);
        Task<Profile> Add(Profile user);
        Task<Profile> Update(Profile user);
        Task<bool> Delete(int id);
    }
}