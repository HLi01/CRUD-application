using J2RXEK_HFT_2021221.Models;
using System.Collections.Generic;

namespace J2RXEK_HFT_2021221.Logic
{
    public interface ITeamLogic
    {
        void Create(Team team);
        Team Read(int id);
        void Update(Team team);
        void Delete(int id);
        IEnumerable<Team> ReadAll();
    }
}
