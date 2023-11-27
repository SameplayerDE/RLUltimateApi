using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RLUltimateApi.Models
{
    public partial class Measurement
    {
        public Measurement()
        {
            Entries = new HashSet<Entry>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PlayerId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public virtual Player Player { get; set; }

        [JsonIgnore]
        public virtual ICollection<Entry> Entries { get; set; }
    }
}
