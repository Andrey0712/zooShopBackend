﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace WebZooShop.Data.Entities
{
    [Table("tblCategory")]
    public class CategoryEntity 
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        public virtual ICollection<ProductEntity> Product { get; set; }
    }
}
