using System.Collections.Generic;
using DAL.Entities;
using DAL.Repository.@interface;
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

        public ValidateResultGroup ValidateNew(Group newGroup)
        {
            var result = new ValidateResultGroup
                         {
                             Id = new ValidateResult(),
                             Name = new ValidateResult()
                         };
            ValidateName(result.Name, newGroup.Name);
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