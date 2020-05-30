using System.Collections.Generic;
using SharedLibrary.Entities;
using SharedLibrary.Models;

namespace DAL.Repository.@interface
{
    public interface IUserRepository
    {
        UserDto Validate(string account, string password);
        
        User[] GetList();
        
        UserDetailViewModel GetDetail(int id);
        
        void New(User newUser);
    }
}