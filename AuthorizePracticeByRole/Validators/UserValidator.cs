using DAL.Repository.@interface;
using SharedLibrary.Entities;
using SharedLibrary.Models;

namespace AuthorizePracticeByRole.Validators
{
    public class UserValidator : IUserValidator
    {
        private readonly IUserRepository _userRepository;

        public UserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public ValidateUserResult ValidateNew(User newUser)
        {
            var result = new ValidateUserResult
                         {
                             Id       = new ValidateResult(),
                             Account  = ValidateAccount(newUser.Account),
                             Password = ValidatePassword(newUser.Password),
                             Name     = ValidateName(newUser.Name),
                         };
            return result;
        }

        private ValidateResult ValidateAccount(string newUserAccount)
        {
            var result = new ValidateResult();
            
            if (string.IsNullOrEmpty(newUserAccount))
            {
                result.IsValid = false;
                result.Errors.Add("Account 不可以為空");
                return result;
            }

            return result;
        }

        private ValidateResult ValidatePassword(string newUserPassword)
        {
            var result = new ValidateResult();
            
            if (string.IsNullOrEmpty(newUserPassword))
            {
                result.IsValid = false;
                result.Errors.Add("Password 不可以為空");
                return result;
            }
            
            return result;
        }

        private ValidateResult ValidateName(string newUserName)
        {
            var result = new ValidateResult();
            
            if (string.IsNullOrEmpty(newUserName))
            {
                result.IsValid = false;
                result.Errors.Add("Name 不可以為空");
                return result;
            }

            return result;
        }
    }
}