using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Transport
    {
        [Column("ship_IMO")]
        public string ShipImo { get; set; }

        [Column("container_BIC")]
        public string ContainerBic { get; set; }

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime? EndDate { get; set; }

        // Navigation properties
        public Ship Ship { get; set; }
        public Container Container { get; set; }
    }
}
