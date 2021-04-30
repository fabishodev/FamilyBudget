using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using FamilyBudget.Entities;
using FamilyBudget.Entities.Dto;

namespace FamilyBudget.Api.Services.Interfaces
{
    public interface IExpenseService
    {
         //Contratos definisdos en el controlador.
        Task<IEnumerable<ExpenseDto>> GetAll();      
        Task<ExpenseDto> Add(ExpenseDto expense);
        Task<ExpenseDto> Update(ExpenseDto expense); 
        Task<IEnumerable<ExpenseDto>> Search(ExpenseSearch search);      
    }
}