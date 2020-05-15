using System.Collections.Generic;

namespace SharedLibrary.Models
{
    public class ValidateResult
    {
        public bool IsValid { get; set; } = true;

        public IList<string> Errors { get; set; } = new List<string>();
    }
}