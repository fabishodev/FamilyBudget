using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using FamilyBudget.Entities;
using FamilyBudget.Api.Repository.Interfaces;
using MySqlConnector;
using Dapper;
using Dapper.Contrib.Extensions;

namespace FamilyBudget.Api.Repository
{
    public class ProfileRepository : IProfileRepository
    {
         private const string _conStr =
            "server=localhost; user=root; pwd=Passw0rd; database=familybudget; port=3306";
        private MySqlConnection _conn;
        public async Task<IEnumerable<Profile>> GetAll()
        {
            string sql = "SELECT * FROM profile WHERE Deleted = 0";       

            using (MySqlConnection connection = new MySqlConnection(_conStr))
            {            
                var profiles =  connection.QueryAsync<Profile>(sql).Result.ToList();
                return profiles;               
            }          
        }

        public async Task<Profile> GetById(int id) 
        {
            var parameters = new { Id = id };
            string sql = "SELECT * FROM profile WHERE id = @Id AND deleted = 0;";

            using (MySqlConnection connection = new MySqlConnection(_conStr))
            {
                var profile = connection.QuerySingleOrDefaultAsync<Profile>(sql, parameters).Result;

                return profile;
            }
        }

        public async Task<Profile> Add(Profile profile)
        {
            string sql = "INSERT INTO profile (name, description) Values (@Name, @Description);";

            using (MySqlConnection connection = new MySqlConnection(_conStr))
            {
                var affectedRows = connection.Execute(sql, new {Name = profile.Name, Description = profile.Description});               

                return profile;
            }
        }

        public async Task<Profile> Update(Profile profile) 
        {
            string sql = "UPDATE profile SET Name = @Name, Description = @Description WHERE id = @Id;";

             using (MySqlConnection connection = new MySqlConnection(_conStr))
            {            
                var affectedRows = connection.Execute(sql,new {Id = profile.Id, Name = profile.Name, Description = profile.Description });

                return profile;
            }
        }

        public async Task<bool> Delete(int id)
        {
            string sql = "UPDATE profile SET Deleted = @Deleted WHERE id = @Id;";

             using (MySqlConnection connection = new MySqlConnection(_conStr))
            {            
                var affectedRows = connection.Execute(sql,new {Id = id, Deleted = 1});

                if(affectedRows > 0){
                    return true;
                }

                return false;

            }
        }


    }
}