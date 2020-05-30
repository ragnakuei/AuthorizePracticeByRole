using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DAL.Repository.@interface;
using Dapper;
using SharedLibrary.Entities;
using SharedLibrary.Models;

namespace DAL.Repository.Dapper
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserDto Validate(string account, string password)
        {
            var sqlScript = @"
SELECT [U].[Id] AS [UserId],
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

        public User[] GetList()
        {
            var sqlScript = @"
SELECT *
FROM [dbo].[User]
";
            using (var conn = new SqlConnection(ConnectionString))
            {
                return conn.Query<User>(sqlScript).ToArray();
            }
        }

        public UserDetailViewModel GetDetail(int id)
        {
            var sqlScript = @"
SELECT [g].[Id], [g].[Name], [g].[Created]
FROM [dbo].[User] [g]
WHERE [Id] = @id

SELECT [g].[Name]
FROM [dbo].[UserGroup] [ug]
    LEFT JOIN [dbo].[Group] [g]
              ON [g].[Id] = [ug].[UserId]
WHERE [ug].[UserId] = @id
";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int32);
            var result = new UserDetailViewModel();
            using (var conn = new SqlConnection(ConnectionString))
            {
                var reader = conn.QueryMultiple(sqlScript, parameters);
                result.User       = reader.Read<User>().FirstOrDefault();
                result.GroupNames = reader.Read<string>().ToArray();
            }

            return result;
        }

        public void New(User newUser)
        {
            var sqlScript = @"
INSERT INTO [dbo].[User] ([Account],[Password],[Name], [Created])
VALUES (@account, @password, @name, @created);
";
            var parameters = new DynamicParameters();
            parameters.Add("Account",  newUser.Account,  DbType.String, size : 50);
            parameters.Add("Password", newUser.Password, DbType.String, size : 100);
            parameters.Add("Name",     newUser.Name,     DbType.String, size : 50);
            parameters.Add("Created",  DateTime.Now,     DbType.DateTime);
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Execute(sqlScript, parameters);
            }
        }
    }
}