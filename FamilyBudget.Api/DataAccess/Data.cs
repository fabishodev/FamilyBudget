using System;
using System.Data;
using System.Data.Common;
using Dapper;
using Microsoft.Extensions.Configuration;
using FamilyBudget.Api.DataAccess.Interfaces;
using MySqlConnector;


namespace FamilyBudget.Api.DataAccess
{
    public class Data : IData
    {
        private readonly IConfiguration _config;
        private string _connectionString = "DatabaseConnection";
        private MySqlConnection _conn;
        public Data(IConfiguration config)
        {
            _config = config;
        }
        public DbConnection DbConnection
        {
            get
            {
                if(_conn == null)
                {
                    _conn = new MySqlConnection(_config.GetConnectionString(_connectionString));
                }

                return _conn;
            }
        }


        
    }
}