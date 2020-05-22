using System.Collections.Generic;
using SharedLibrary.Entities;

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