using System.Threading.Tasks;
using System.Collections.Generic;
using FamilyBudget.Entities;
using FamilyBudget.Entities.Dto;
using System.Data.Common;

namespace FamilyBudget.Api.Repository.Interfaces
{
    public interface IExpenseRepository
    {
        DbConnection DbConnection{get; }
         Task<IEnumerable<ExpenseDto>> GetAll();       
         Task<ExpenseDto> Add(ExpenseDto expense);
         Task<ExpenseDto> Update(ExpenseDto expense);
         Task<IEnumerable<ExpenseDto>> Search(ExpenseSearch search);
        
    }
}