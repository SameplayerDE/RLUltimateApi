using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Diagnostics.Metrics;

namespace RLUltimateApi.Models
{
    public partial class Player
    {
        public Player()
        {
            Measurements = new HashSet<Measurement>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Measurement> Measurements { get; set; }
    }
}
