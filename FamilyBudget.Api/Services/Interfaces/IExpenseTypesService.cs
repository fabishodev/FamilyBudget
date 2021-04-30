using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using FamilyBudget.Entities;

namespace FamilyBudget.Api.Services.Interfaces
{
    public interface IExpenseTypesService
    {
         Task<IEnumerable<ExpenseType>> GetAll();   
    }
}