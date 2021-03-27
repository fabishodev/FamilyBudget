using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
 
using FamilyBudget.Api.Services.Interfaces;
 
namespace FamilyBudget.Api.Middleware
{
    public class JwtMiddleware
    {
 
        private readonly RequestDelegate _next;
 
        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }
 
        public async Task Invoke(HttpContext context,IAuthService authService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault().Split(" ").Last();
 
            if(token != null)
                AttachUsertToContext(context,authService,token);
 
            await _next(context);
 
        }
 
        private void AttachUsertToContext(HttpContext context,IAuthService authService, string token)
        {
            try
            {
                var secret = "THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACTE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING";
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = System.Text.Encoding.ASCII.GetBytes(secret);
 
                tokenHandler.ValidateToken(token, new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true, 
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false , 
                    ClockSkew = TimeSpan.Zero, 
                }, out SecurityToken validatedToken);
 
                var jwtToken = (JwtSecurityToken) validatedToken;
 
                var userId = int.Parse(jwtToken.Claims.First( x => x.Type == "id").Value);
 
                context.Items["User"] =  authService.GetUserBydId(userId).Result;
                
            }
            catch
            {
 
            }
        }
 
    }
}