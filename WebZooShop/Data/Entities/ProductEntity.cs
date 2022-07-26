using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebZooShop.Data.Entities.Identity;

namespace WebZooShop.Data.Entities
{
    [Table("tblProducts")]
    public class ProductEntity : BaseEntity<int>
    {
        [Display(Name = "Назва"), Required(ErrorMessage = "Поле 'Назва' не може бути пустим!")]
        public string Name { get; set; }
        [Display(Name = "Ціна"), Required(ErrorMessage = "Поле 'Ціна' не може бути пустим!")]
        public int Price { get; set; }
        [Display(Name = "Опис товару"), Required(ErrorMessage = "Поле 'Опис товару' не може бути пустим!")]
        public string Description { get; set; }
        [Display(Name = "Титульна фотографія")]
        public string StartPhoto { get; set; }     
        public int Rating { get; set; }
        //public int? Quantity { get; set; }
        //public string InventoryStatus { get; set; }
        [Display(Name = "Категория")]
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public virtual CategoryEntity Category { get; set; }
        public virtual ICollection<CartEntity> CartEntities { get; set; }
        //public virtual ICollection<ProductImageEntity> ProductImages { get; set; }
        [Display(Name = "Наявність товару")]
        [ForeignKey("InventoryStatus")]
        public int? InventoryStatusId { get; set; }
        public virtual InventoryStatusEntity InventoryStatus { get; set; }

    }
}
