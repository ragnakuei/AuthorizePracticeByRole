using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DAL.Entities;
using DAL.Repository.@interface;
using Dapper;
using SharedLibrary.Helpers;

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
    }
}