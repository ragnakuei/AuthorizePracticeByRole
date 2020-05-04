using SharedLibrary.Models;

namespace DAL.Repository
{
    public interface IUserRepository
    {
        UserDto Validate(string account, string password);
    }
}
