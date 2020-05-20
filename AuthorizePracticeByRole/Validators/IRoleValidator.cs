using DAL.Entities;
using SharedLibrary.Models;

namespace AuthorizePracticeByRole.Validators
{
    public interface IRoleValidator
    {
        ValidateResultRole ValidateNew(RoleValidateModel newRole);
        
        string ValidateName(string name);
    }
}