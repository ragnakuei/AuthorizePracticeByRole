using DAL.Repository.@interface;
using SharedLibrary.Entities;
using SharedLibrary.Models;

namespace AuthorizePracticeByRole.Validators
{
    public class GroupValidator : IGroupValidator
    {
        private readonly IGroupRepository _groupRepository;

        public GroupValidator(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public ValidateGroupResult ValidateNew(Group newGroup)
        {
            var result = new ValidateGroupResult
                         {
                             Id = new ValidateResult(),
                             Name = ValidateName(newGroup.Name)
                         };
            return result;
        }

        private ValidateResult ValidateName(string name)
        {
            var result = new ValidateResult();
            
            if (string.IsNullOrEmpty(name))
            {
                result.IsValid = false;
                result.Errors.Add("Name 不可以為空");
                return result;
            }

            var minLength = 2;
            if (name.Length < minLength)
            {
                result.IsValid = false;
                result.Errors.Add($"Name 長度不可以小於 {minLength}");
            }

            var maxLength = 50;
            if (name.Length > maxLength)
            {
                result.IsValid = false;
                result.Errors.Add($"Name 長度不可以大於 {maxLength}");
            }

            return result;
        }
    }
}