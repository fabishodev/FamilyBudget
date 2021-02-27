using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using FamilyBudget.Entities;

namespace FamilyBudget.Api.Services.Interfaces
{
    public interface IProfileService
    {
        Task<IEnumerable<Profile>> GetAll();
        Task<Profile> GetById(int id);
        Task<Profile> Add(Profile profile);
        Task<Profile> Update(Profile profile);
        Task<bool> Delete(int id);
    }
}