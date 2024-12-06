using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Port
    {
        [Key]
        [Column("UN_code")]
        public string UnCode { get; set; }

        [Column("port_name")]
        public string PortName { get; set; }

        [Column("country")]
        public string Country { get; set; }

        [Column("latitude")]
        public decimal Latitude { get; set; }

        [Column("longitude")]
        public decimal Longitude { get; set; }

        [Column("docking_fee_usd")]
        public decimal DockingFeeUsd { get; set; }

        [Column("mooring_fee_usd")]
        public decimal MooringFeeUsd { get; set; }

        [Column("harbor_pilot_fee_usd")]
        public decimal HarborPilotFeeUsd { get; set; }

        [Column("tugboat_fee_usd")]
        public decimal TugboatFeeUsd { get; set; }

        [Column("terminal_handling_fee_usd")]
        public decimal TerminalHandlingFeeUsd { get; set; }

        [Column("agency_fee_usd")]
        public decimal AgencyFeeUsd { get; set; }

        [Column("inspection_fee_usd")]
        public decimal InspectionFeeUsd { get; set; }
    }
}
