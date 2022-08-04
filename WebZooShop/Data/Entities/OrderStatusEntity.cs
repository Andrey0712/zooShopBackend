using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebZooShop.Data.Entities
{
    [Table("tblOrderStatusEntities")]
    public class OrderStatusEntity : BaseEntity<int>
    {
        [StringLength(200)]
        public string Name { get; set; }

        public virtual ICollection<OrderEntity> Orders { get; set; }
    }
}
