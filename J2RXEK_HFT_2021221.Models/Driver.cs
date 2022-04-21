using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace J2RXEK_HFT_2021221.Models
{
    public class Driver
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Number { get; set; }

        public int Age { get; set; }

        public string Name { get; set; }

        public string DebutYear { get; set; }

        public bool IsChampion { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Team Team { get; set; }

        [ForeignKey(nameof(Team))]
        public int TeamId { get; set; }
    }
}
