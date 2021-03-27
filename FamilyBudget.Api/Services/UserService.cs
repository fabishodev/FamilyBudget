using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using FamilyBudget.Entities;
using FamilyBudget.Entities.Dto;

using FamilyBudget.Api.Services.Interfaces;
using FamilyBudget.Api.Repository.Interfaces;


namespace FamilyBudget.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            await _userRepository.DbConnection.OpenAsync();
            var users = await _userRepository.GetAll();
            //var users = new List<User>();
            await _userRepository.DbConnection.CloseAsync();
            return users;
        }

         public async Task<UserDto> GetById(int id)
         {
             await _userRepository.DbConnection.OpenAsync();
             var user = await _userRepository.GetById(id);
             await _userRepository.DbConnection.CloseAsync();
             return user;
            // return new User{ Id = id};
         }
         public async Task<UserDto> Add(UserDto user)
         {
             await _userRepository.DbConnection.OpenAsync();
             await _userRepository.Add(user);
             await _userRepository.DbConnection.CloseAsync();
             return user;
         }

         public async Task<UserDto> Update(UserDto user)
         {
             await _userRepository.DbConnection.OpenAsync();
             await _userRepository.Update(user);
             await _userRepository.DbConnection.CloseAsync();
             return user;
         }

          public async Task<bool> Delete(int id)
          {
              await _userRepository.DbConnection.OpenAsync();
              bool result = await _userRepository.Delete(id);
              await _userRepository.DbConnection.CloseAsync();
             return result;
         }
        
    }
}