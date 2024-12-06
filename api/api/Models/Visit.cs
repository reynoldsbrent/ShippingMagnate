using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Visit
    {
        [Column("ship_IMO")]
        public string ShipImo { get; set; }

        [Column("port_code")]
        public string PortCode { get; set; }

        [Column("visit_date")]
        public DateTime VisitDate { get; set; }

        // Navigation properties
        public Ship Ship { get; set; }
        public Port Port { get; set; }
    }
}
