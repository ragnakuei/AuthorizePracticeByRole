using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DAL.Entities;
using DAL.Repository.@interface;
using Dapper;
using SharedLibrary.Helpers;
using SharedLibrary.Models;

namespace DAL.Repository
{
    public class AuthorizeRepository : BaseRepository, IAuthorizeRepository
    {
        public bool Auth(AuthorizationDto dto)
        {
            var sqlScript  = $"EXECUTE {SpStore.AuthorizeUserIdRoles} @userId, @attributeRoles";
            var parameters = new DynamicParameters();
            parameters.Add("userId", dto.UserId, DbType.Int32);

            var attributeRolesDataTable = new DataTableStringWithSerialGenerator("Value");
            attributeRolesDataTable.AddRange(dto.AttributeRoles);
            parameters.Add("attributeRoles", attributeRolesDataTable.Result.AsTableValuedParameter("[dbo].[Nvarchar1000]"));

            using (var conn = new SqlConnection(ConnectionString))
            {
                var result = conn.Query<AuthResultDto>(sqlScript, parameters)
                                 .FirstOrDefault(r => r.UserId == dto.UserId)
                                ?.AuthResult;
                return result ?? false;
            }
        }
    }
}