using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using SharedLibrary.Models;

namespace DAL.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserDto Validate(string account, string password)
        {
            var sqlScript = @"
SELECT [U].[id] AS [UserId],
       [U].[Account],
       [U].[Name],
       [U].[Created]
FROM [dbo].[User] [U]
WHERE [U].[Account] = @account
  AND [U].[Password] = @password

SELECT [R].[Name]
FROM [dbo].[User] [U]
    JOIN [UserGroup] [UG]
         ON [U].[Id] = [UG].[UserId]
    JOIN [GroupRole] [GR]
         ON [UG].[GroupId] = [GR].[GroupId]
    JOIN [Role] [R]
         ON [GR].[RoleId] = [R].[Id]
WHERE [U].[Account] = @account
  AND [U].[Password] = @password";
            var parameters = new DynamicParameters();
            parameters.Add("account",  account,  DbType.String, size : 50);
            parameters.Add("password", password, DbType.String, size : 100);

            using (var conn = new SqlConnection(ConnectionString))
            {
                var reader = conn.QueryMultiple(sqlScript, parameters);
                var result = reader.Read<UserDto>().FirstOrDefault();
                result.Roles = reader.Read<string>().ToArray();
                return result;
            }
        }
    }
}