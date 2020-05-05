using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DAL.EF;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Helpers;
using SharedLibrary.Models;

namespace DAL.Repository
{
    public class AuthorizeRepository : IAuthorizeRepository
    {
        private readonly AuthorizePracticeDbContext _dbContext;

        public AuthorizeRepository(AuthorizePracticeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Auth(AuthorizationDto dto)
        {
            var userId = new SqlParameter
                         {
                             ParameterName = "@UserId",
                             DbType        = DbType.Int32,
                             IsNullable    = false,
                             Value         = dto.UserId
                         };

            var rolesDataTable = new DataTableStringWithSerialGenerator("Value");
            rolesDataTable.AddRange(dto.AttributeRoles);

            var roles = new SqlParameter
                        {
                            ParameterName = "@Roles",
                            SqlDbType     = SqlDbType.Structured,
                            Value         = rolesDataTable.Result,
                            TypeName      = "[dbo].[Nvarchar1000]"
                        };

            var result = _dbContext.AuthResultDtos
                                   .FromSqlInterpolated($"EXECUTE [dbo].[usp_AuthorizeUserIdRoles] {userId}, {roles}")
                                   .AsEnumerable()
                                   .FirstOrDefault(r => r.UserId == dto.UserId)
                                  ?.AuthResult;

            return result ?? false;
        }
    }
}