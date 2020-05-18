using System.Collections.Generic;
using DAL.Entities;

namespace DAL.Repository.@interface
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetList();

        void New(Role role);

        void Update(Role role);

        void Delete(int id);
    }
}