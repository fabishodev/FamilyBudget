using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using FamilyBudget.Entities;
using FamilyBudget.Entities.Dto;

namespace FamilyBudget.Api.Services.Interfaces
{
    public interface IUserService
    {
        //Contratos definisdos en el controlador.
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto> GetById(int id);
        Task<UserDto> Add(UserDto user);
        Task<UserDto> Update(UserDto user);
        Task<bool> Delete(int id);
        
    }
}