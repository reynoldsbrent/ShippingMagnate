using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Handle
    {
        [Column("port_code")]
        public string PortCode { get; set; }

        [Column("container_BIC")]
        public string ContainerBic { get; set; }

        [Column("handling_date")]
        public DateTime HandlingDate { get; set; }

        // Navigation properties
        public Port Port { get; set; }
        public Container Container { get; set; }
    }
}
