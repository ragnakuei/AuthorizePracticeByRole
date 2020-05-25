using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using DAL.Repository.@interface;
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

        public void Update(Group updateGroup)
        {
            updateGroup.Created = DateTime.Now;

            using (var dbContext = DbContextFactory.CreateEfDbContext())
            {
                var groupInDb = dbContext.Groups.Find(updateGroup.Id);
                dbContext.Entry(groupInDb).CurrentValues.SetValues(updateGroup);
                dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var dbContext = DbContextFactory.CreateEfDbContext())
            {
                var deleteGroup = dbContext.Groups.Find(id);
                dbContext.Entry(deleteGroup).State = EntityState.Deleted;
                dbContext.SaveChanges();
            }
        }
    }
}