using J2RXEK_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J2RXEK_HFT_2021221.Repository
{
    public interface ITeamRepository
    {
        void Create(Team team);
        void Update(Team team);
        Team Read(string TeamName);
        IQueryable<Team> ReadAll();
        void Delete(string TeamName);
    }
}
