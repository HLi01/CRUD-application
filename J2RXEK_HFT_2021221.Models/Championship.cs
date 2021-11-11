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
        [NotMapped]
        public static int[] points = new int[] { 25, 18, 15, 12, 10, 8, 6, 4, 2, 1 };

        [Key]
        public string RaceID { get; set; }

        public string Location { get; set; }
        [Required]
        public List<Driver> result { get; set; }

        public DateTime Date { get; set; }

        [NotMapped]
        public virtual ICollection<Team> Teams { get; set; }

        public Championship()
        {
            Teams = new HashSet<Team>();
            result = new List<Driver>();
        }
    }
    
}
