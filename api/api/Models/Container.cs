using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Container
    {
        [Key]
        [Column("BIC_code")]
        public string BicCode { get; set; }

        [Column("size_TEU")]
        public decimal SizeTeu { get; set; }

        [Column("is_empty")]
        public bool IsEmpty { get; set; }

        [Column("current_weight_kg")]
        public decimal CurrentWeightKg { get; set; }

        [Column("load_capacity_kg")]
        public decimal LoadCapacityKg { get; set; }

        [Column("total_volume_m3")]
        public decimal TotalVolumeM3 { get; set; }

        [Column("used_volume_m3")]
        public decimal UsedVolumeM3 { get; set; }
    }
}
