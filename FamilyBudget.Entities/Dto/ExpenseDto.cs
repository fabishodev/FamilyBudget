using System;
namespace FamilyBudget.Entities
{
    public class ExpenseDto : Expense 
    {
        public User User { get; set; }
        public ExpenseType ExpenseType { get; set; }
    }
}