using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using FamilyBudget.Entities;
using FamilyBudget.Entities.Dto;

using FamilyBudget.Api.Services.Interfaces;
using FamilyBudget.Api.Repository.Interfaces;

namespace FamilyBudget.Api.Services
{
    public class ExpenseTypesService : IExpenseTypesService
    {
        private readonly IExpenseTypesRepository _expenseTypesRepository;

        public ExpenseTypesService(IExpenseTypesRepository expenseTypesRepository)
        {
            _expenseTypesRepository = expenseTypesRepository;
        }
        public async Task<IEnumerable<ExpenseType>> GetAll()
        {
            await _expenseTypesRepository.DbConnection.OpenAsync();
            var types = await _expenseTypesRepository.GetAll();
            await _expenseTypesRepository.DbConnection.CloseAsync();
            return types;
        }
    }
}