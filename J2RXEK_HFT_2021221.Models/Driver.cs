using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J2RXEK_HFT_2021221.Models
{
    public class Driver
    {
        [Key]
        public int Number { get; set; }

        public string Name { get; set; }

        public DateTime DebutYear { get; set; }

        public bool IsChampion { get; set; }

        [NotMapped]
        public virtual Team Team { get; set; }

        [ForeignKey (nameof(Team))]
        public int TeamName { get; set; }

    }
}
