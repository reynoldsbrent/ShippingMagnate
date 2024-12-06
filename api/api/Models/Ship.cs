using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Ship
    {
        [Key]
        [Column("IMO_number")]
        public string ImoNumber { get; set; }

        [Column("ship_name")]
        public string ShipName { get; set; }

        [Column("container_capacity")]
        public int ContainerCapacity { get; set; }
    }
}
