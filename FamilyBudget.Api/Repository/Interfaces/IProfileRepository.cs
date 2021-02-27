using System.Threading.Tasks;
using System.Collections.Generic;
using FamilyBudget.Entities;

namespace FamilyBudget.Api.Repository.Interfaces
{
    public interface IProfileRepository 
    {
        Task<IEnumerable<Profile>> GetAll();
        Task<Profile> GetById(int id);
        Task<Profile> Add(Profile user);
        Task<Profile> Update(Profile user);
        Task<bool> Delete(int id);
    }
}