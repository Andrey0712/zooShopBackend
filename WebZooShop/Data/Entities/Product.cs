﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebZooShop.Data.Entities.Identity;

namespace WebZooShop.Data.Entities
{
    [Table("tblProducts")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, StringLength(100)]
        public int Price { get; set; }
        [Required, StringLength(4000)]
        public string Description { get; set; }
        [StringLength(255)]
        public string StartPhoto { get; set; }
        public DateTime DateCreate { get; set; }
        
        [ForeignKey("ProductCategory")]
        public int ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<UserProduct> UserProduct { get; set; }
    }
}
