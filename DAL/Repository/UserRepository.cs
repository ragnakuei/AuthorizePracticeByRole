using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SharedLibrary.Models;

namespace DAL.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserDto Validate(string account, string password)
        {
            var sqlScript = @"";
            var parameters = new DynamicParameters();
            
            using (var conn = new SqlConnection(ConnectionString))
            {
                var result = conn.Query<UserDto>(sqlScript, ).ToList();
            }
            
            var user = _dbContext.User
                                 .Where(u => u.Account  == account
                                          && u.Password == password)
                                 .Select(u => new UserDto
                                              {
                                                  UserId      = u.Id,
                                                  Account = u.Account,
                                                  Name    = u.Name,
                                                  Created = u.Created,
                                                  Roles = u.UserGroups
                                                           .SelectMany(ug => ug.Group.GroupRoles)
                                                           .Select(gr => gr.Role.Name)
                                                           .ToArray()
                                              })
                                 .FirstOrDefault();
            return user;
        }
    }
}