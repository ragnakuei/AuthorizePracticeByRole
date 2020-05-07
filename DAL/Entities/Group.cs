using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
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