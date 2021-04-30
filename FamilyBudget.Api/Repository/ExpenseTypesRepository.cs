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
    public class ExpenseTypesRepository : IExpenseTypesRepository
    {
        private readonly IData _data;

        public DbConnection DbConnection
        {
            get
            {
                return _data.DbConnection;
            }
        }
        public ExpenseTypesRepository(IData data)
        {
            _data = data;
        }

        public async Task<IEnumerable<ExpenseType>> GetAll()
        {
            var types = await _data.DbConnection.GetAllAsync<ExpenseType>();
            return types;            
        }

    }
}