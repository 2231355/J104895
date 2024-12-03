using System.ComponentModel.DataAnnotations.Schema;

namespace J104895.Models
{
    public class Order
    {
        public int Id { get; set; }
        public required string CustomerName { get; set; }
        public required string CustomerEmail { get; set; }
        public required string Address { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public required List<OrderItem> OrderItems { get; set; }
    }
}
