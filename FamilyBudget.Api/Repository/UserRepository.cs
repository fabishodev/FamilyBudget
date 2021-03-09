using System.Threading.Tasks;
using System.Collections.Generic;
using FamilyBudget.Entities;
using FamilyBudget.Api.Repository.Interfaces;
using MySqlConnector;
using Dapper;
using Dapper.Contrib.Extensions;
using FamilyBudget.Api.DataAccess.Interfaces;
using System.Data.Common;

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


        public async Task<User> Add(User user)
        {
           await _data.DbConnection.InsertAsync<User>(user);           
           return user;
        }

        public async Task<bool> Delete(int id)
        {    

            var result = await _data.DbConnection.DeleteAsync<User>(new User{Id = id});
            return result;
        }

        public async Task<IEnumerable<User>> GetAll()
        {    
            var users = await _data.DbConnection.GetAllAsync<User>();    
            return users;
;
        }
        public async Task<User> GetById(int id) 
        {   
            var user = await _data.DbConnection.GetAsync<User>(id);         
            return user;
        }

        public async Task<User> Update(User user) 
        {
            await _data.DbConnection.UpdateAsync<User>(user); 
            return user;
        }
    }
}