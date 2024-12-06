using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Schedule
    {
        [Column("ship_IMO")]
        public string ShipImo { get; set; }

        [Column("arrival_port_code")]
        public string ArrivalPortCode { get; set; }

        [Column("departure_port_code")]
        public string DeparturePortCode { get; set; }

        [Column("arrival_date")]
        public DateTime? ArrivalDate { get; set; }

        [Column("departure_date")]
        public DateTime DepartureDate { get; set; }

        // Navigation properties
        public Ship Ship { get; set; }
        public Port ArrivalPort { get; set; }
        public Port DeparturePort { get; set; }
    }
}
