using System.Linq;
using DAL.EF;
using SharedLibrary.Models;

namespace DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AccountDbContext _dbContext;

        public UserRepository(AccountDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserDto Validate(string account, string password)
        {
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