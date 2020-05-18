using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DAL.Entities;
using DAL.Repository.@interface;
using Dapper;

namespace DAL.Repository
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        public IEnumerable<Role> GetList()
        {
            var sqlScript = @"
SELECT *
FROM [dbo].[Role]
";
            using (var conn = new SqlConnection(ConnectionString))
            {
                return conn.Query<Role>(sqlScript);
            }
        }

        public void New(Role role)
        {
            var sqlScript = @"
INSERT INTO [dbo].[Role] ([Name], [Created])
VALUES (@name, @created);
";
            var parameters = new DynamicParameters();
            parameters.Add("Name",    role.Name,       DbType.String, size: 50);
            parameters.Add("Created", DateTime.Now, DbType.DateTime);
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Execute(sqlScript, parameters);
            }
        }

        public void Update(Role role)
        {
            var sqlScript = @"
UPDATE [dbo].[Role]
SET [Name]    = @Name,
    [Created] = @Created
WHERE [Id] = @Id
";
            var parameters = new DynamicParameters();
            parameters.Add("Id",      role.Id,         DbType.Int32);
            parameters.Add("Name",    role.Name,       DbType.String, size: 50);
            parameters.Add("Created", DateTime.Now, DbType.DateTime);
            
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Execute(sqlScript, parameters);
            }
        }

        public void Delete(int id)
        {
            var sqlScript = @"
DELETE FROM [dbo].[Role]
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