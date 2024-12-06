using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class ShipContainer
    {
        [Column("customer_id")]
        public string CustomerId { get; set; }

        [Column("container_BIC")]
        public string ContainerBic { get; set; }

        [Column("shipping_date")]
        public DateTime ShippingDate { get; set; }

        // Navigation properties
        public Customer Customer { get; set; }
        public Container Container { get; set; }
    }
}
