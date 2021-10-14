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
        public string Location { get; set; }

        public DateTime Date { get; set; }

        [NotMapped]
        public virtual ICollection<Team> Teams { get; set; }

        public Championship()
        {
            Teams = new HashSet<Team>();
        }
    }
}
