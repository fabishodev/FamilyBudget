using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using FamilyBudget.Api.Services.Interfaces; 
using FamilyBudget.Api.Services;

using FamilyBudget.Api.Repository;
using FamilyBudget.Api.Repository.Interfaces;

using FamilyBudget.Api.DataAccess.Interfaces;
using FamilyBudget.Api.DataAccess;

using FamilyBudget.Api.Middleware;

namespace FamilyBudget.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FamilyBudget.Api", Version = "v1" });
            });

            
            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IExpenseService, ExpenseService>();//IExpenseTypesService
            services.AddScoped<IExpenseTypesService, ExpenseTypesService>();

            //Repositories
            //services.AddScoped<IUserRepository, UserFakeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IExpenseTypesRepository, ExpenseTypesRepository>();

            services.AddScoped<IData, Data>();

            services.AddSingleton<IConfiguration>(Configuration);


             services.AddCors(opt => {
                opt.AddPolicy("CorsPolicy", policy => { 

                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");

                });
            });


            ConfigureSqlMapperExtensions();

        }

        public void ConfigureSqlMapperExtensions()
        {
            Dapper.Contrib.Extensions.SqlMapperExtensions.TableNameMapper = entityType  =>
            {
                const string nms = "FamilyBudget.Entities.";
                var name = entityType.ToString();
 
                if(name.Contains(nms))
                {
                    name = name.Replace(nms,"");
                }
 
                name = TopUpperFirstLetter(name);
 
                return name;
            };
    
        }

          private string TopUpperFirstLetter(string source)
        {
            if(string.IsNullOrEmpty(source))
                return string.Empty;
 
            char[] letters = source.ToCharArray();
 
            letters[0] = char.ToUpper(letters[0]);
 
            return new string(letters);
 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FamilyBudget.Api v1"));
            }

            app.ConfigureExceptionHandler();            

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            //app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}
