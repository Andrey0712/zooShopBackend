using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebZooShop.Data.Entities.Identity;

namespace WebZooShop.Data.Entities
{
    [Table("tblOrderEntities")]
    public class OrderEntity : BaseEntity<int>
    {
        /// <summary>
        /// Контакти отримувача
        /// </summary>
        [StringLength(100)]
        public string ConsumerFirstName { get; set; }
        [StringLength(100)]
        public string ConsumerSecondName { get; set; }
        [StringLength(20)]
        public string ConsumerPhone { get; set; }

        [ForeignKey("OrderStatus")]
        public int StatusId { get; set; }

        /// <summary>
        /// Користувач який змовив
        /// </summary>
        [ForeignKey("User")]
        public long UserId { get; set; }

        public virtual OrderStatusEntity OrderStatus { get; set; }
        public virtual AppUser User { get; set; }
        public virtual ICollection<OrderItemEntity> OrderItems { get; set; }
    }
}
