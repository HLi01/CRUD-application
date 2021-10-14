using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J2RXEK_HFT_2021221.Models
{
    public class Championship
    {
        [Key]
        public DateTime Year { get; set; }

        [NotMapped]
        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }

        public Championship()
        {
            Teams = new HashSet<Team>();
            Drivers = new HashSet<Driver>();
        }

        [ForeignKey(nameof(Teams))]
        public string TeamName { get; set; }

        [ForeignKey(nameof(Drivers))]
        public int Number { get; set; }
    }
}
