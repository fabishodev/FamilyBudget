using System.Threading.Tasks;
using System.Collections.Generic;
using FamilyBudget.Entities;
using FamilyBudget.Entities.Dto;
using FamilyBudget.Api.Repository.Interfaces;
using MySqlConnector;
using Dapper;
using Dapper.Contrib.Extensions;
using FamilyBudget.Api.DataAccess.Interfaces;
using System.Data.Common;
using System.Linq;

namespace FamilyBudget.Api.Repository
{   
    public class UserRepository : IUserRepository
    {
        // private const string _conStr =
        //     "server=localhost; user=root; pwd=Passw0rd; database=familybudget; port=3306";
        //private MySqlConnection _conn;
        private readonly IData _data;
        public DbConnection DbConnection 
        {
            get{
                return _data.DbConnection;
            }
        }
        public UserRepository(IData data)
        {
            _data = data;
        }

        public async Task<UserDto> Add(UserDto user)
        {
           await _data.DbConnection.InsertAsync<User>(user);           
           return user;
        }

        public async Task<bool> Delete(int id)
        {    

            var result = await _data.DbConnection.DeleteAsync<User>(new User{Id = id});
            return result;
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {  
            var sql = @"SELECT 
                            u.Id,
                            u.FirstName,
                            u.LastName,
                            u.UserName,
                            u.ProfileId,
                            p.Id, 
                            p.Name,
                            p.Description
                        FROM 
                            User u
                        INNER JOIN 
                            Profile p
                                ON (u.ProfileId = p.Id)";

            var users = await _data.DbConnection.QueryAsync<User,Profile,UserDto> (
                sql,
                (user,profile) =>{
                    return new UserDto{
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserName = user.UserName,
                        ProfileId = profile.Id,
                        Profile = profile,
                    };
                },
                new {},
                splitOn : "ProfileId"
            );
            

            //var users = await _data.DbConnection.GetAllAsync<User>();    
            return users;
;
        }
        public async Task<UserDto> GetById(int id) 
        {   
            var sql = @"SELECT 
                            u.Id,
                            u.FirstName,
                            u.LastName,
                            u.UserName,
                            u.ProfileId,
                            p.Id, 
                            p.Name,
                            p.Description
                        FROM 
                            User u
                        INNER JOIN 
                            Profile p
                                ON (u.ProfileId = p.Id)
                        WHERE u.Id = @UserId";

            var users = await _data.DbConnection.QueryAsync<User,Profile,UserDto> (
                sql,
                (user,profile) =>{
                    return new UserDto{
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserName = user.UserName,
                        ProfileId = profile.Id,
                        Profile = profile,
                    };
                },
                new {UserId = id},
                splitOn : "ProfileId"
            );

            var user = users.FirstOrDefault();
            //var user = await _data.DbConnection.GetAsync<User>(id);         
            return user;
        }

        public async Task<UserDto> Update(UserDto user) 
        {
            await _data.DbConnection.UpdateAsync<User>(user); 
            return user;
        }
    }
}