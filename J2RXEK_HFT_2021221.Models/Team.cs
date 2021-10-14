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
        public string Name { get; set; }

        public string TeamPrincipals { get; set; }

        public string PowerUnit { get; set; }

        public int ChampionshipsWon { get; set; }

        [NotMapped]
        public virtual ICollection<Driver> Drivers { get; set; }
        public virtual ICollection<Championship> Championship_Years { get; set; }

        public Team()
        {
            Drivers = new HashSet<Driver>();
        }

        [ForeignKey(nameof(Drivers))]
        public int Number { get; set; }

        [ForeignKey(nameof(Championship_Years))]
        public DateTime Year { get; set; }
    }
}
