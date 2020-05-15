namespace SharedLibrary.Models
{
    public class ValidateResultGroup
    {
        public bool IsValid => Id.IsValid && Name.IsValid;

        public ValidateResult Id   { get; set; }
        public ValidateResult Name { get; set; }
    }
}