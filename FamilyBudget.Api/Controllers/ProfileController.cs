using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FamilyBudget.Entities;
using FamilyBudget.Api.Services.Interfaces;
using FamilyBudget.Api.Services;

namespace FamilyBudget.Api.Controllers
{
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

         //GET api/profile
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<ActionResult<IEnumerable<Profile>>> GetAll()
        {
            var profiles = await _profileService.GetAll();
            return Ok(profiles);//200
        }

         //GET api/profile/id
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<ActionResult<Profile>> GetById(int id)
        {
            var profile = await _profileService.GetById(id);
            return Ok(profile);
        }

        //POST api/profile
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult<Profile>> Add([FromBody] Profile profile){
            await _profileService.Add(profile);
            return profile;
        }

        //PUT api/profile
        [HttpPut]
        [Route("api/[controller]")]
        public async Task<ActionResult<Profile>> Update([FromBody]Profile profile)
        {
            await _profileService.Update(profile);
            return Created($"api/profile/{profile.Id}", profile);   
        }

        //DELETE api/profile
        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _profileService.Delete(id);
            return result;
        }
    }
}