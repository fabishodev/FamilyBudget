using System.Data;
using System.Data.Common;
using Dapper;

namespace FamilyBudget.Api.DataAccess.Interfaces
{
    public interface IData
    {
         DbConnection DbConnection{get;}
    }
}