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
        Team Read(int id);
        IQueryable<Team> ReadAll();
        void Delete(int id);
    }
}
