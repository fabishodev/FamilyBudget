using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using FamilyBudget.Entities;
using FamilyBudget.Api.Repository.Interfaces;
using MySqlConnector;
using Dapper;
using Dapper.Contrib.Extensions;

using FamilyBudget.Api.DataAccess.Interfaces;
using System.Data.Common;

namespace FamilyBudget.Api.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        //  private const string _conStr =
        //     "server=localhost; user=root; pwd=Passw0rd; database=familybudget; port=3306";
        // private MySqlConnection _conn;
        private readonly IData _data;
        public DbConnection DbConnection 
        {
            get{
                return _data.DbConnection;
            }
        }
        public ProfileRepository(IData data)
        {
            _data = data;
        }
        public async Task<IEnumerable<Profile>> GetAll()
        {
            // _conn = new MySqlConnection(_conStr);
            // _conn.Open();
            //var profiles = await _conn.GetAllAsync<Profile>();
            //profiles = profiles.Where(w => w.Deleted == false);

            var template = new Profile{Deleted = false};
            var parameters = new DynamicParameters(template);
            var sql = "SELECT * FROM profile WHERE Deleted = @Deleted;";

            var profiles = await _data.DbConnection.QueryAsync<Profile>(sql, parameters);
            //_conn.Close();
            return profiles;         
        }

        public async Task<Profile> GetById(int id) 
        {          

            //var profiles = await _conn.GetAllAsync<Profile>();
            //profiles = profiles.Where(w => w.Deleted == false);

            var template = new Profile{Id = id, Deleted = false};
            var parameters = new DynamicParameters(template);
            var sql = "SELECT * FROM profile WHERE id = @Id AND Deleted = @Deleted;";

            var profiles = await _data.DbConnection.QueryAsync<Profile>(sql, parameters);

            var profile = profiles.FirstOrDefault();

         

            return profile;  
        }

        public async Task<Profile> Add(Profile profile)
        {            

           await _data.DbConnection.InsertAsync<Profile>(profile);          
           return profile;
        }

        public async Task<Profile> Update(Profile profile) 
        {         

            await _data.DbConnection.UpdateAsync<Profile>(profile);
            return profile;
        }

        public async Task<bool> Delete(int id)
        {
           
            var profile = await GetById(id);

            if(profile == null)
                return false;

            profile.Deleted = true;
            await _data.DbConnection.UpdateAsync<Profile>(profile);        

            return true;
        }


    }
}