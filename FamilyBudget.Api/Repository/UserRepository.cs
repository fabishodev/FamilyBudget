using System.Threading.Tasks;
using System.Collections.Generic;
using FamilyBudget.Entities;
using FamilyBudget.Api.Repository.Interfaces;
using MySqlConnector;
using Dapper;
using Dapper.Contrib.Extensions;

namespace FamilyBudget.Api.Repository
{   
    public class UserRepository : IUserRepository
    {
         private const string _conStr =
            "server=localhost; user=root; pwd=Passw0rd; database=familybudget; port=3306";

        private MySqlConnection _conn;
        public async Task<User> Add(User user)
        {
           _conn = new MySqlConnection(_conStr);
           _conn.Open();

           await _conn.InsertAsync<User>(user);
           _conn.Close();

           return user;
        }

        public async Task<bool> Delete(int id)
        {
            _conn = new MySqlConnection(_conStr);
            _conn.Open();

            var result = await _conn.DeleteAsync<User>(new User{Id = id});
            _conn.Close();

            return result;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            _conn = new MySqlConnection(_conStr);
            _conn.Open();

            var users = await _conn.GetAllAsync<User>();

            _conn.Close();

            return users;
;
        }
        public async Task<User> GetById(int id) 
        {
            _conn = new MySqlConnection(_conStr);
            _conn.Open();

            var user = await _conn.GetAsync<User>(id);

            _conn.Close();

            return user;
        }

        public async Task<User> Update(User user) 
        {
             _conn = new MySqlConnection(_conStr);
            _conn.Open();

            await _conn.UpdateAsync<User>(user);

            _conn.Close();

            return user;
        }
    }
}