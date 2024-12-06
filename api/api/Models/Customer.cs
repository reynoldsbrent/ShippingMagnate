using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Customer
    {
        [Key]
        [Column("customer_id")]
        public string CustomerId { get; set; }

        [Column("customer_name")]
        public string CustomerName { get; set; }

        [Column("contact_name")]
        public string ContactName { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("billing_address")]
        public string BillingAddress { get; set; }
    }
}
