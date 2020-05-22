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

        public ValidateResultRole ValidateNew(RoleValidateModel newRole)
        {
            var result = new ValidateResultRole
                         {
                             Id   = new ValidateResult(),
                             Name = new ValidateResult()
                         };
            ValidateName(result.Name, newRole.Name);
            return result;
        }

        public string ValidateName(string name)
        {
            var result = new ValidateResult();
            ValidateName(result, name);
            return string.Join(",", result.Errors);
        }
        
        private void ValidateName(ValidateResult validateResult, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                validateResult.IsValid = false;
                validateResult.Errors.Add("Name 不可以為空");
                return;
            }

            var minLength = 3;
            if (name.Length < minLength)
            {
                validateResult.IsValid = false;
                validateResult.Errors.Add($"Name 長度不可以小於 {minLength}");
            }

            var maxLength = 9;
            if (name.Length > maxLength)
            {
                validateResult.IsValid = false;
                validateResult.Errors.Add($"Name 長度不可以大於 {maxLength}");
            }
        }
    }
}