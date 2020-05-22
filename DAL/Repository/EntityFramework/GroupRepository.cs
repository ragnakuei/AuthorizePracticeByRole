using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DAL.Repository.@interface;
using Dapper;
using SharedLibrary.Entities;
using SharedLibrary.Models;

namespace DAL.Repository.EntityFramework
{
    public class GroupRepository : IGroupRepository
    {
        public IEnumerable<Group> GetList()
        {
            using (var dbContext = new EfDbContext())
            {
                return dbContext.Groups.ToArray();
            }
        }

        public GroupDetailViewModel GetDetail(int id)
        {
            var sqlScript = @"
SELECT [g].[Id], [Name], [Created]
FROM [dbo].[Group] [g]
WHERE [Id] = @id

SELECT [u].[Name]
FROM [dbo].[UserGroup] [ug]
    LEFT JOIN [dbo].[User] [u]
              ON [u].[Id] = [ug].[UserId]
WHERE [ug].[GroupId] = @id

SELECT [r].[Name]
FROM [dbo].[GroupRole] [gr]
    LEFT JOIN [dbo].[Role] [r]
              ON [gr].[RoleId] = [r].[Id]
WHERE [gr].[GroupId] = @id
";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int32);
            var result = new GroupDetailViewModel();
            using (var conn = new SqlConnection(ConnectionString))
            {
                var reader =  conn.QueryMultiple(sqlScript, parameters);
                result.Group = reader.Read<Group>().FirstOrDefault();
                result.UserNames = reader.Read<string>();
                result.RoleNames = reader.Read<string>();
            }
            return result;
        }

        public string ConnectionString { get; set; }

        public void New(Group g)
        {
            var sqlScript = @"
INSERT INTO [dbo].[Group] ([Name], [Created])
VALUES (@name, @created);
";
            var parameters = new DynamicParameters();
            parameters.Add("Name",    g.Name,       DbType.String, size : 50);
            parameters.Add("Created", DateTime.Now, DbType.DateTime);
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Execute(sqlScript, parameters);
            }
        }

        public void Update(Group g)
        {
            var sqlScript = @"
UPDATE [dbo].[Group]
SET [Name]    = @Name,
    [Created] = @Created
WHERE [Id] = @Id
";
            var parameters = new DynamicParameters();
            parameters.Add("Id",      g.Id,         DbType.Int32);
            parameters.Add("Name",    g.Name,       DbType.String, size : 50);
            parameters.Add("Created", DateTime.Now, DbType.DateTime);

            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Execute(sqlScript, parameters);
            }
        }

        public void Delete(int id)
        {
            var sqlScript = @"
DELETE FROM [dbo].[Group]
WHERE Id = @id
";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int32);

            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Execute(sqlScript, parameters);
            }
        }
    }
}