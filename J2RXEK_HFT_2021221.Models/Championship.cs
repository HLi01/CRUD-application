using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace J2RXEK_HFT_2021221.Models
{
    public class Championship
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Year { get; set; }

        public int NumberOfRaces { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Team Team { get; set; }

        [ForeignKey(nameof(Team))]
        public int WCC { get; set; }
    }
    
}
