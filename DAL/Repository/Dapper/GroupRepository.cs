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
    public class GroupRepository : BaseRepository, IGroupRepository
    {
        public Group[] GetList()
        {
            var sqlScript = @"
SELECT *
FROM [dbo].[Group]
";
            using (var conn = new SqlConnection(ConnectionString))
            {
                return conn.Query<Group>(sqlScript).ToArray();
            }
        }

        public GroupDetailViewModel GetDetail(int id)
        {
            var sqlScript = @"
SELECT [g].[Id], [g].[Name], [g].[Created]
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
                var reader = conn.QueryMultiple(sqlScript, parameters);
                result.Group     = reader.Read<Group>().FirstOrDefault();
                result.UserNames = reader.Read<string>().ToArray();
                result.RoleNames = reader.Read<string>().ToArray();
            }

            return result;
        }

        public void New(Group newGroup)
        {
            var sqlScript = @"
INSERT INTO [dbo].[Group] ([Name], [Created])
VALUES (@name, @created);
";
            var parameters = new DynamicParameters();
            parameters.Add("Name",    newGroup.Name, DbType.String, size : 50);
            parameters.Add("Created", DateTime.Now,  DbType.DateTime);
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Execute(sqlScript, parameters);
            }
        }

        public void Update(Group updateGroup)
        {
            var sqlScript = @"
UPDATE [dbo].[Group]
SET [Name]    = @Name,
    [Created] = @Created
WHERE [Id] = @Id
";
            var parameters = new DynamicParameters();
            parameters.Add("Id",      updateGroup.Id,   DbType.Int32);
            parameters.Add("Name",    updateGroup.Name, DbType.String, size : 50);
            parameters.Add("Created", DateTime.Now,     DbType.DateTime);

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