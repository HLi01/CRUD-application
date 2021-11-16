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
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string TeamName { get; set; }

        public string TeamPrincipal { get; set; }

        public string PowerUnit { get; set; }

        public int ChampionshipsWon { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Driver> Drivers { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Championship> Championships { get; set; }

        public Team()
        {
            Drivers = new HashSet<Driver>();
            Championships = new HashSet<Championship>();
        }
        public override bool Equals(object obj)
        {
            return TeamName == (obj as Team).TeamName;
        }
        //[ForeignKey(nameof(Championship))]
        //public int RaceID { get; set; }

    }
}
