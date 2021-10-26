using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J2RXEK_HFT_2021221.Models
{
    public class Team
    {
        [Key]
        public string TeamName { get; set; }

        public string TeamPrincipal { get; set; }

        public string PowerUnit { get; set; }

        public int ChampionshipsWon { get; set; }

        [NotMapped]
        public virtual ICollection<Driver> Drivers { get; set; }
        [NotMapped]
        public virtual Championship Championship { get; set; }

        public Team()
        {
            Drivers = new HashSet<Driver>();
        }

        [ForeignKey(nameof(Championship))]
        public string RaceID { get; set; }

    }
}
