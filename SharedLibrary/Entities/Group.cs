using System;
using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.Entities
{
    public class Group
    {
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Display(Name = "名稱")]
        public string Name { get; set; }

        [Display(Name = "建立時間")]
        public DateTime Created { get; set; }
    }
}