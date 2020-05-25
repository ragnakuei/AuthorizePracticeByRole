using System.Collections.Generic;
using SharedLibrary.Entities;
using SharedLibrary.Models;

namespace DAL.Repository.@interface
{
    public interface IGroupRepository
    {
        IEnumerable<Group> GetList();

        GroupDetailViewModel GetDetail(int id);

        void New(Group g);

        void Update(Group updateGroup);

        void Delete(int id);
    }
}