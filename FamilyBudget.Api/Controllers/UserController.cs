using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FamilyBudget.Entities;
using FamilyBudget.Entities.Dto;
using FamilyBudget.Api.Services.Interfaces; 
using FamilyBudget.Api.Services;
using FamilyBudget.Api.Authorization;

namespace FamilyBudget.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //GET api/user
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);//200
        }

        //GET api/user/id
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var user = await _userService.GetById(id);
            return Ok(user);
        }

        //POST api/user
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult<UserDto>> Add([FromBody] UserDto user){
            await _userService.Add(user);
            return user;
        }

        [HttpPut]
        [Route("api/[controller]")]
        public async Task<ActionResult<UserDto>> Update([FromBody]UserDto user)
        {
            await _userService.Update(user);
            return Created($"api/user/{user.Id}",user);   
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _userService.Delete(id);
            return result;
        }

    }
}