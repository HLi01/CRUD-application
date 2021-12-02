using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public virtual ICollection<Driver> Drivers { get; set; }

        [NotMapped]
        public virtual ICollection<Championship> Championships { get; set; }

        public Team()
        {
            Drivers = new HashSet<Driver>();
            Championships = new HashSet<Championship>();
        }
    }
}
