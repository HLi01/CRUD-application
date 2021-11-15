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
        public string RaceID { get; set; }

        public string Location { get; set; }

        public string WinnerName { get; set; }

        public DateTime Date { get; set; }

        [NotMapped]
        public virtual ICollection<Team> Teams { get; set; }

        public Championship()
        {
            Teams = new HashSet<Team>();
        }
    }
    
}
