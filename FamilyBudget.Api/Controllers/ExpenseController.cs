using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FamilyBudget.Entities;
using FamilyBudget.Entities.Dto;
using FamilyBudget.Api.Services.Interfaces; 
using FamilyBudget.Api.Services;
using FamilyBudget.Api.Authorization;

namespace FamilyBudget.Api.Controllers
{
     [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly IExpenseTypesService _expenseTypesService;
         public ExpenseController(IExpenseService expenseService, IExpenseTypesService expenseTypesService)
         {
             _expenseService = expenseService;
             _expenseTypesService = expenseTypesService;
         }

         //GET api/expenses
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetAll()
        {
            var expenses = await _expenseService.GetAll();
            return Ok(expenses);//200
        }

         //GET api/expenses
        [HttpGet]
        [Route("api/[controller]/types")]
        public async Task<ActionResult<IEnumerable<ExpenseType>>> GetAllTypes()
        {
            var types = await _expenseTypesService.GetAll();
            return Ok(types);//200
        }

        [HttpPut]
        [Route("api/[controller]")]
        public async Task<ActionResult<ExpenseDto>> Update([FromBody]ExpenseDto expense)
        {
            await _expenseService.Update(expense);
            return Created($"api/expense/{expense.Id}",expense);   
        }

        //POST api/user
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult<ExpenseDto>> Add([FromBody] ExpenseDto expense)
        {
            await _expenseService.Add(expense);
            return expense;
        }

        //POST api/user
        [HttpPost]
        [Route("api/[controller]/search")]
        public async Task<ActionResult<ExpenseDto>> Search([FromBody] ExpenseSearch expense)
        {
            var expenses = await _expenseService.Search(expense);
            return Ok(expenses);
        }
    }
}