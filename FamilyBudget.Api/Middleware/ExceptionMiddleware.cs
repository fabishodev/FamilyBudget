using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using FamilyBudget.Entities;
using Newtonsoft.Json;

namespace FamilyBudget.Api.Middleware
{
     public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app) 
        {
             app.UseExceptionHandler(appError => {
                appError.Run( async context => {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if(contextFeature != null)
                    {
                       
                        var error = new ErrorDetails(){
                            Message = $"Internal Server Error{contextFeature.Error.Message}"
                        };
                        await context.Response.WriteAsJsonAsync(error);
                    }
                });
            });
        }           
                
    }
}