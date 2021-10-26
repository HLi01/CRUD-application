using J2RXEK_HFT_2021221.Data;
using J2RXEK_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J2RXEK_HFT_2021221.Repository
{
    public class ChampionshipRepository : IChampionshipRepository
    {
        ChampionshipDBContext db;
        public ChampionshipRepository(ChampionshipDBContext db)
        {
            this.db = db;
        }
        public void Create(Championship race)
        {
            db.Championship_2021.Add(race);
            db.SaveChanges();
        }

        public void Delete(string RaceID)
        {
            var RaceToDelete = Read(RaceID);
            db.Championship_2021.Remove(RaceToDelete);
            db.SaveChanges();
        }

        public Championship Read(string RaceID)
        {
            return db.Championship_2021.FirstOrDefault(t => t.RaceID == RaceID);
        }

        public IQueryable<Championship> ReadAll()
        {
            return db.Championship_2021;
        }

        public void Update(Championship race)
        {
            var OldRace = Read(race.RaceID);
            OldRace.Location = race.Location;
            OldRace.Date = race.Date;
            db.SaveChanges();

        }
    }
}
