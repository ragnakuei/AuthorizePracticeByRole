namespace SharedLibrary.Models
{
    public class ValidateUserResult
    {
        /// <summary>
        /// 整體的驗証結果
        /// </summary>
        public bool IsValid => Id.IsValid 
                            && Account.IsValid
                            && Password.IsValid
                            && Name.IsValid;

        public ValidateResult Id { get; set; }
        
        public ValidateResult Account { get; set; }
        
        public ValidateResult Password { get; set; }
        
        public ValidateResult Name { get; set; }
    }
}