using System.Threading.Tasks;
using System.Collections.Generic;
using FamilyBudget.Entities;
using System.Data.Common;


namespace FamilyBudget.Api.Repository.Interfaces
{
    public interface IExpenseTypesRepository
    {
        DbConnection DbConnection{get; }
        Task<IEnumerable<ExpenseType>> GetAll();    
    }
}