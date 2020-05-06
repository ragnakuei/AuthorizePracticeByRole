using SharedLibrary.Models;

namespace DAL.Repository.@interface
{
    public interface IUserRepository
    {
        UserDto Validate(string account, string password);
    }
}
