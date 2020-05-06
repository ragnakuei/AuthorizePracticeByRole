using System.Collections.Generic;
using DAL.Entities;

namespace DAL.Repository.@interface
{
    public interface IGroupRepository
    {
        IEnumerable<Group> GetList();
    }
}