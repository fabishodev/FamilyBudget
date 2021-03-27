using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using FamilyBudget.Entities;
using FamilyBudget.Entities.Dto;
using FamilyBudget.Api.Services.Interfaces; 
using FamilyBudget.Api.Services;

namespace FamilyBudget.Api.Controllers
{
     [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;


        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult<UserSession>> Auth([FromBody] Auth auth)
        {

            if(auth == null)
                return BadRequest();
                
            var session = await _authService.Auth(auth);
            return session;

        } 



    }
}