using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Remark { get; set; }

        public DateTime? Created { get; set; }
    }
}