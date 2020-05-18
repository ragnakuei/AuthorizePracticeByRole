using DAL.Entities;
using SharedLibrary.Models;

namespace AuthorizePracticeByRole.Validators
{
    public interface IRoleValidator
    {
        ValidateResultRole ValidateNew(Role newRole);
    }
}