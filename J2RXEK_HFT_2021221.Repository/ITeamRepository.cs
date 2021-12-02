using J2RXEK_HFT_2021221.Models;
using System.Linq;

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
