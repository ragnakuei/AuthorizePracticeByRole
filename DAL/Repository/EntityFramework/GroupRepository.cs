using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
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
            using (var dbContext = DbContextFactory.CreateEfDbContext())
            {
                return dbContext.Groups.ToArray();
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
            var result = new GroupDetailViewModel();
            using (var dbContext = DbContextFactory.CreateEfDbContext())
            {
                var command = dbContext.Database.Connection.CreateCommand();
                command.CommandText = sqlScript;
                command.CommandType = CommandType.Text;
                command.Parameters.Clear();

                var parameter = new SqlParameter(parameterName : "id", value : id)
                                {
                                    SqlDbType = SqlDbType.Int
                                };
                command.Parameters.Add(parameter);

                dbContext.Database.Connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    var objectContextAdapter = ((IObjectContextAdapter)dbContext).ObjectContext;

                    result.Group = objectContextAdapter.Translate<Group>(reader).FirstOrDefault();
                    reader.NextResult();
                    result.UserNames = objectContextAdapter.Translate<string>(reader).ToArray();
                    reader.NextResult();
                    result.RoleNames = objectContextAdapter.Translate<string>(reader).ToArray();
                }
            }

            return result;
        }

        public string ConnectionString { get; set; }

        public void New(Group g)
        {
            using (var dbContext = DbContextFactory.CreateEfDbContext())
            {
                dbContext.Groups.Add(g);
                dbContext.SaveChanges();
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