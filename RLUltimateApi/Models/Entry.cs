using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RLUltimateApi.Models
{
    public partial class Entry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int MeasurementId { get; set; }

        [Required]
        public int TypeId { get; set; }

        public int? Value { get; set; }

        [JsonIgnore]
        public virtual EntryType Type { get; set; }

        [JsonIgnore]
        public virtual Measurement Measurement { get; set; }
    }
}
