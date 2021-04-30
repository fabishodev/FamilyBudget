using System;
namespace FamilyBudget.Entities
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Total { get; set; }
        public int RegisterById { get; set; }
        public int ExpenseTypeId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public DateTime CreatedDate { get; set; } 
        public DateTime UpdatedDate { get; set; }  

    }
}