using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using FamilyBudget.Entities;

using FamilyBudget.Api.Services.Interfaces;
using FamilyBudget.Api.Repository.Interfaces;

namespace FamilyBudget.Api.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<IEnumerable<Profile>> GetAll()
        {
            await _profileRepository.DbConnection.OpenAsync();
            var profiles = await _profileRepository.GetAll();
            await _profileRepository.DbConnection.CloseAsync();
            return profiles;
        }
        public async Task<Profile> GetById(int id)
         {
             await _profileRepository.DbConnection.OpenAsync();
             var profile = await _profileRepository.GetById(id);
             await _profileRepository.DbConnection.CloseAsync();
             return profile;            
         }

         public async Task<Profile> Add(Profile profile)
         {
             await _profileRepository.DbConnection.OpenAsync();
             await _profileRepository.Add(profile);
             await _profileRepository.DbConnection.CloseAsync();
             return profile;
         }

         public async Task<Profile> Update(Profile profile)
         {
             await _profileRepository.DbConnection.OpenAsync();
             await _profileRepository.Update(profile);
             await _profileRepository.DbConnection.CloseAsync();
             return profile;
         }

         public async Task<bool> Delete(int id)
         {
             await _profileRepository.DbConnection.OpenAsync();
             bool result = await _profileRepository.Delete(id);
             await _profileRepository.DbConnection.CloseAsync();
             return result;
         }
    }
}