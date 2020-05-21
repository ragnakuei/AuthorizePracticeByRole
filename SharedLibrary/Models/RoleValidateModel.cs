using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SharedLibrary.Models
{
    public class RoleValidateModel
    {
        public int Id { get; set; }
        
        [Remote(action : "ValidateNew"
              , controller : "Roles"
              , AdditionalFields = "Id,__RequestVerificationToken"
              , HttpMethod       = "Post")]
        [Required]
        [DisplayName("名稱")]
        [StringLength(10
                    , MinimumLength = 2
                    , ErrorMessage  = "{0} 至少要 {2} 字元，不超過 {1} 字元")]
        public string Name { get; set; }
    }
}