using SharedLibrary.Entities;
using SharedLibrary.Models;

namespace AuthorizePracticeByRole.Validators
{
    public interface IUserValidator
    {
        ValidateUserResult ValidateNew(User newUser);
    }
}