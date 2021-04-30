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
using System;

namespace FamilyBudget.Api.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly IData _data;

        public DbConnection DbConnection
        {
            get
            {
                return _data.DbConnection;
            }
        }

        public ExpenseRepository(IData data)
        {
            _data = data;
        }

        public async Task<ExpenseDto> Add(ExpenseDto expense)
        {
            expense.CreatedDate = DateTime.Now;
            await _data.DbConnection.InsertAsync<Expense>(expense);
            return expense;
        }

        public async Task<ExpenseDto> Update(ExpenseDto expense)
        {

            // Expense e = new Expense();
            // e.Id = expense.Id;
            // e.Description = expense.Description;
            // e.Total = expense.Total;
            // e.RegisterById = expense.RegisterById;
            // e.ExpenseTypeId = expense.ExpenseType.Id;
            // e.ExpenseDate = expense.ExpenseDate;
            expense.UpdatedDate = DateTime.Now;

            await _data.DbConnection.UpdateAsync<Expense>(expense);
            return expense;
        }

        public async Task<IEnumerable<ExpenseDto>> GetAll()
        {
            var sql = @"SELECT 
                            e.Id,
                            e.Description,
                            e.Total,
                            e.RegisterById,
                            DATE_FORMAT(e.ExpenseDate, '%Y-%m-%d') AS ExpenseDate,
                            e.CreatedDate,
                            e.UpdatedDate,
                            u.Id,
                            u.FirstName,
                            u.LastName,
                            u.UserName,
                            et.Id,
                            et.Description                           
                        FROM 
                            Expense e
                        INNER JOIN 
                            User u
                                ON (e.RegisterById =  u.Id)
                        INNER JOIN 
                            ExpenseType et
                                ON (e.ExpenseTypeId =  et.Id)";

            var expenses = await _data.DbConnection.QueryAsync<Expense, User, ExpenseType, ExpenseDto>(
                sql,
                (expense, user, expensetype) =>
                {
                    return new ExpenseDto
                    {
                        Id = expense.Id,
                        Description = expense.Description,
                        Total = expense.Total,
                        RegisterById = user.Id,
                        ExpenseTypeId = expensetype.Id,
                        ExpenseDate = expense.ExpenseDate,
                        CreatedDate = expense.CreatedDate,
                        UpdatedDate = expense.UpdatedDate,
                        User = user,
                        ExpenseType = expensetype
                    };
                },
                new { },
                splitOn: "Id, Id"
            );


            //var users = await _data.DbConnection.GetAllAsync<User>();    
            return expenses;
            ;
        }

        public async Task<IEnumerable<ExpenseDto>> Search(ExpenseSearch search)
        {
            var sql = @"SELECT 
                            e.Id,
                            e.Description,
                            e.Total,
                            e.RegisterById,
                            DATE_FORMAT(e.ExpenseDate, '%Y-%m-%d') AS ExpenseDate,
                            e.CreatedDate,
                            e.UpdatedDate,
                            u.Id,
                            u.FirstName,
                            u.LastName,
                            u.UserName,
                            et.Id,
                            et.Description                            
                        FROM 
                            Expense e
                        INNER JOIN 
                            User u
                                ON (e.RegisterById =  u.Id)
                        INNER JOIN 
                            ExpenseType et
                                ON (e.ExpenseTypeId =  et.Id)";

            if (search.ExpenseDateFrom != "" && search.ExpenseDateTo != "" && search.RegisterById != 0)
            {
                sql += "WHERE e.ExpenseDate >= '" + search.ExpenseDateFrom + "' AND e.ExpenseDate <= '" + search.ExpenseDateTo + "' AND e.RegisterById = " + search.RegisterById + "";
            }        

            if (search.ExpenseDateFrom != "" && search.ExpenseDateTo != "" && search.RegisterById == 0)
            {
                sql += "WHERE e.ExpenseDate >= '" + search.ExpenseDateFrom + "' AND e.ExpenseDate <= '" + search.ExpenseDateTo + "'";
            }   

            if (search.ExpenseDateFrom == "" && search.ExpenseDateTo == "" && search.RegisterById != 0)
            {
                sql += "WHERE e.RegisterById = " + search.RegisterById + "";
            } 

            var expenses = await _data.DbConnection.QueryAsync<Expense, User, ExpenseType, ExpenseDto>(
                sql,
                (expense, user, expensetype) =>
                {
                    return new ExpenseDto
                    {
                        Id = expense.Id,
                        Description = expense.Description,
                        Total = expense.Total,
                        RegisterById = user.Id,
                        ExpenseTypeId = expensetype.Id,
                        ExpenseDate = expense.ExpenseDate,
                        CreatedDate = expense.CreatedDate,
                        UpdatedDate = expense.UpdatedDate,
                        User = user,
                        ExpenseType = expensetype
                    };
                },
                new { },
                splitOn: "Id, Id"
            );


            //var users = await _data.DbConnection.GetAllAsync<User>();    
            return expenses;
            ;
        }

    }
}