﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IRouteRoleRepository
    {
        string[] Get(string controllerName, string actionName);
    }
}
