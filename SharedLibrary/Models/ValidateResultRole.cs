namespace SharedLibrary.Models
{
    public class ValidateResultRole
    {
        /// <summary>
        /// 整體的驗証結果
        /// </summary>
        public bool IsValid => Id.IsValid && Name.IsValid;

        public ValidateResult Id   { get; set; }
        public ValidateResult Name { get; set; }
    }
}