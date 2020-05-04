using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DAL.EF;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;

namespace DAL.Repository
{
    public class AuthorizeRepository : IAuthorizeRepository
    {
        private readonly AccountDbContext _dbContext;

        public AuthorizeRepository(AccountDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public bool Auth(AuthorizationDto dto)
        {
            var sqlScript = @"EXECUTE [dbo].[AuthorizeUserIdAndFunctionActionPairs] @UserId, @AuthorizeFunctionActionPairs";
            
            var authorizeFunctionActionPairs = new SqlParameter("@AuthorizeFunctionActionPairs", SqlDbType.Structured);
            authorizeFunctionActionPairs.Value = dto.AuthorizeFunctionActionPairs;
            authorizeFunctionActionPairs.TypeName = "[dbo].[AuthorizeFunctionActionPairs]";
            
            var parameterDataTable = new SqlParameter
                                     {
                                         ParameterName = "@input",
                                         SqlDbType     = SqlDbType.Structured,
                                         TypeName      = "[dbo].[tIdName]",
                                         Value         = GenerateDataTable()
                                     };
            
            var parameters = new
            {
                UserId = dto.UserId,
                AuthorizeFunctionActionPairs = authorizeFunctionActionPairs
            };
            var result = _dbContext.AuthResultDtos.FromSqlRaw(sqlScript, parameters).FirstOrDefault();
            return result.AuthResult;
        }
    }
}