using SharedLibrary.Entities;
using SharedLibrary.Models;

namespace AuthorizePracticeByRole.Validators
{
    public interface IGroupValidator
    {
        ValidateGroupResult ValidateNew(Group newGroup);
    }
}