using System.ComponentModel.DataAnnotations.Schema;

namespace WebZooShop.Data.Entities
{
    [Table("tblOrderItemEntities")]
    public class OrderItemEntity : BaseEntity<int>
    {
        /// <summary>
        /// Кількість товару, який ми замовили
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Замовлення
        /// </summary>
        [ForeignKey("Order")]
        public int OrderId { get; set; }

        /// <summary>
        /// Продукт, який замовив
        /// </summary>
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public int BuyPrice { get; set; }
        public int Suma { get; set; }
        public virtual OrderEntity Order { get; set; }
        public virtual ProductEntity Product { get; set; }



    }
}
