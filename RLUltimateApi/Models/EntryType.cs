using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RLUltimateApi.Models
{
    public partial class EntryType
    {
        public EntryType()
        {
            Entries = new HashSet<Entry>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Identifier { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<Entry> Entries { get; set; }
    }
}
