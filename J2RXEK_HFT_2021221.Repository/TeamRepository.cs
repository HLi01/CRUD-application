using J2RXEK_HFT_2021221.Data;
using J2RXEK_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Delete(string TeamName)
        {
            var TeamToDelete = Read(TeamName);
            db.Teams.Remove(TeamToDelete);
            db.SaveChanges();
        }

        public Team Read(string TeamName)
        {
            return db.Teams.FirstOrDefault(t => t.TeamName == TeamName);
        }

        public IQueryable<Team> ReadAll()
        {
            return db.Teams;
        }

        public void Update(Team team)
        {
            var OldTeam = Read(team.TeamName);
            OldTeam.TeamPrincipal = team.TeamPrincipal;
            OldTeam.PowerUnit = team.PowerUnit;
            OldTeam.Championship = team.Championship;
            db.SaveChanges();
        }
    }
}
