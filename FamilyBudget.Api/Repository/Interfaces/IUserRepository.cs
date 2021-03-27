using System.Threading.Tasks;
using System.Collections.Generic;
using FamilyBudget.Entities;
using FamilyBudget.Entities.Dto;
using System.Data.Common;

namespace FamilyBudget.Api.Repository.Interfaces
{
    public interface IUserRepository
    {
        DbConnection DbConnection{get; }
         Task<IEnumerable<UserDto>> GetAll();
         Task<UserDto> GetById(int id);
         Task<UserDto> Add(UserDto user);
         Task<UserDto> Update(UserDto user);
         Task<bool> Delete(int id);
    }
}