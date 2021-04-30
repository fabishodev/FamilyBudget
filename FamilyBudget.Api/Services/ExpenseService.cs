using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using FamilyBudget.Entities;
using FamilyBudget.Entities.Dto;

using FamilyBudget.Api.Services.Interfaces;
using FamilyBudget.Api.Repository.Interfaces;

namespace FamilyBudget.Api.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

         public async Task<ExpenseDto> Add(ExpenseDto expense)
         {
             await _expenseRepository.DbConnection.OpenAsync();
             await _expenseRepository.Add(expense);
             await _expenseRepository.DbConnection.CloseAsync();
             return expense;
         }

         public async Task<ExpenseDto> Update(ExpenseDto expense)
         {
             await _expenseRepository.DbConnection.OpenAsync();
             await _expenseRepository.Update(expense);
             await _expenseRepository.DbConnection.CloseAsync();
             return expense;
         }

         public async Task<IEnumerable<ExpenseDto>> GetAll()
        {
            await _expenseRepository.DbConnection.OpenAsync();
            var expenses = await _expenseRepository.GetAll();
            //var users = new List<User>();
            await _expenseRepository.DbConnection.CloseAsync();
            return expenses;
        }

        public async Task<IEnumerable<ExpenseDto>> Search(ExpenseSearch search)
        {
            await _expenseRepository.DbConnection.OpenAsync();
            var expenses = await _expenseRepository.Search(search);
            //var users = new List<User>();
            await _expenseRepository.DbConnection.CloseAsync();
            return expenses;
        }
    }
}