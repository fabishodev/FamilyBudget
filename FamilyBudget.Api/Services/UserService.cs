using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using FamilyBudget.Entities;

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

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _userRepository.GetAll();
            //var users = new List<User>();
            return users;
        }

         public async Task<User> GetById(int id)
         {
             var user = await _userRepository.GetById(id);
             return user;
            // return new User{ Id = id};
         }
         public async Task<User> Add(User user)
         {
             await _userRepository.Add(user);
             return user;
         }

         public async Task<User> Update(User user)
         {
             await _userRepository.Update(user);
             return user;
         }

          public async Task<bool> Delete(int id)
          {
              bool result = await _userRepository.Delete(id);
             return result;
         }
        
    }
}