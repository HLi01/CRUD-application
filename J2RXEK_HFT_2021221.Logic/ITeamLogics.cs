using J2RXEK_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J2RXEK_HFT_2021221.Logic
{
    public interface ITeamLogics
    {
        void Create(Team team);
        Team Read(string name);
        void Update(Team team);
        void Delete(string name);
        IEnumerable<Team> ReadAll();
    }
}
