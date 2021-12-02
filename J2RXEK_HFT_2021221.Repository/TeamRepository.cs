using J2RXEK_HFT_2021221.Data;
using J2RXEK_HFT_2021221.Models;
using System.Linq;

namespace J2RXEK_HFT_2021221.Repository
{
    public class TeamRepository : ITeamRepository
    {
        ChampionshipDBContext db;
        public TeamRepository(ChampionshipDBContext db)
        {
            this.db = db;
        }

        public void Create(Team team)
        {
            db.Teams.Add(team);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var TeamToDelete = Read(id);
            db.Teams.Remove(TeamToDelete);
            db.SaveChanges();
        }

        public Team Read(int id)
        {
            return db.Teams.FirstOrDefault(t => t.Id == id);
        }

        public IQueryable<Team> ReadAll()
        {
            return db.Teams;
        }

        public void Update(Team team)
        {
            var OldTeam = Read(team.Id);
            OldTeam.TeamName = team.TeamName;
            OldTeam.TeamPrincipal = team.TeamPrincipal;
            OldTeam.PowerUnit = team.PowerUnit;
            OldTeam.ChampionshipsWon = team.ChampionshipsWon;
            db.SaveChanges();
        }
    }
}
