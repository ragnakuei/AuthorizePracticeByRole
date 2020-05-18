using DAL.Entities;
using DAL.Repository.@interface;
using SharedLibrary.Models;

namespace AuthorizePracticeByRole.Validators
{
    public class RoleValidator : IRoleValidator
    {
        private readonly IRoleRepository _groupRepository;

        public RoleValidator(IRoleRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public ValidateResultRole ValidateNew(Role newRole)
        {
            var result = new ValidateResultRole
                         {
                             Id   = new ValidateResult(),
                             Name = new ValidateResult()
                         };
            ValidateName(result.Name, newRole.Name);
            return result;
        }

        private void ValidateName(ValidateResult validateResult, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                validateResult.IsValid = false;
                validateResult.Errors.Add("Name 不可以為空");
                return;
            }

            var minLength = 2;
            if (name.Length < minLength)
            {
                validateResult.IsValid = false;
                validateResult.Errors.Add($"Name 長度不可以小於 {minLength}");
            }

            var maxLength = 50;
            if (name.Length > maxLength)
            {
                validateResult.IsValid = false;
                validateResult.Errors.Add($"Name 長度不可以大於 {maxLength}");
            }
        }
    }
}