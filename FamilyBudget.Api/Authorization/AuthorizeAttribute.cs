using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using FamilyBudget.Entities;


 
namespace FamilyBudget.Api.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User) context.HttpContext.Items["User"];
 
            if(user!= null) return;
 
            context.Result = new JsonResult(new { Succedd = false, Errors = new string[]{"Unauthorized"}}){
                StatusCode = StatusCodes.Status401Unauthorized
            };
 
        }
    }
}