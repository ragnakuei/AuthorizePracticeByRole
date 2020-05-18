using System.Collections.Generic;
using DAL.Entities;

namespace DAL.Repository.@interface
{
    public interface IGroupRepository
    {
        IEnumerable<Group> GetList();

        void New(Group g);

        void Update(Group g);

        void Delete(int id);
    }
}