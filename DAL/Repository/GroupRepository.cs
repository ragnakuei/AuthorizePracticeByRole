using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DAL.Entities;
using DAL.Repository.@interface;
using Dapper;

namespace DAL.Repository
{
    public class GroupRepository : BaseRepository, IGroupRepository
    {
        public IEnumerable<Group> GetList()
        {
            var sqlScript = @"
SELECT *
FROM [dbo].[Group]
";
            using (var conn = new SqlConnection(ConnectionString))
            {
                return conn.Query<Group>(sqlScript);
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
            parameters.Add("Id", g.Id, DbType.Int32);
            parameters.Add("Name", g.Name, DbType.String, size: 50);
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