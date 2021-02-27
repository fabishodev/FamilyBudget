using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using FamilyBudget.Entities;

namespace FamilyBudget.Api.Services.Interfaces
{
    public interface IUserService
    {
        //Contratos definisdos en el controlador.
        Task<IEnumerable<User>> GetAll();
         Task<User> GetById(int id);
         Task<User> Add(User user);
         Task<User> Update(User user);
        Task<bool> Delete(int id);
        
    }
}