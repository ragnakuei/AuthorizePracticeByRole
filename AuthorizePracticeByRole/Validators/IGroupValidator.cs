using DAL.Entities;
using SharedLibrary.Models;

namespace AuthorizePracticeByRole.Validators
{
    public interface IGroupValidator
    {
        ValidateResultGroup ValidateNew(Group newGroup);
    }
}