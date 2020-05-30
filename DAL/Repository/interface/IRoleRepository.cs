using System.Collections.Generic;
using SharedLibrary.Entities;

namespace DAL.Repository.@interface
{
    public interface IRoleRepository
    {
        Role[] GetList();

        void New(Role role);

        void Update(Role role);

        void Delete(int id);
    }
}