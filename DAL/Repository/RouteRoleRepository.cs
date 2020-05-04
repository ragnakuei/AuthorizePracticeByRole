using System;
using System.Linq;
using DAL.EF;

namespace DAL.Repository
{
    public class RouteRoleRepository : IRouteRoleRepository
    {
        private readonly AccountDbContext _dbContext;

        public RouteRoleRepository(AccountDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string[] Get(string controllerName, string actionName)
        {
            var roles = _dbContext?.Route
                                  ?.FirstOrDefault(r => r.Controller == controllerName
                                                     && r.Action == actionName)
                                  ?.RouteRoles
                                   .Select(rr => rr.Role)
                                   .Select(r => r.Name)
                                   .ToArray();
            return roles;
        }
    }
}