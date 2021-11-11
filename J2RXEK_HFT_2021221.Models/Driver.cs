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

        public string DebutYear { get; set; }

        public bool IsChampion { get; set; }

        [NotMapped]
        public virtual Team Team { get; set; }

        [ForeignKey (nameof(Team))]
        public string TeamName { get; set; }
        
        public override bool Equals(object obj)
        {
            return Name == (obj as Driver).Name;
        }
    }
}
