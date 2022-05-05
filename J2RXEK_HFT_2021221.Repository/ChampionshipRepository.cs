using J2RXEK_HFT_2021221.Data;
using J2RXEK_HFT_2021221.Models;
using System.Linq;

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
            db.Championship.Add(race);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var RaceToDelete = Read(id);
            db.Championship.Remove(RaceToDelete);
            db.SaveChanges();
        }

        public Championship Read(int id)
        {
            return db.Championship.FirstOrDefault(t => t.Id == id);
        }

        public IQueryable<Championship> ReadAll()
        {
            return db.Championship;
        }

        public void Update(Championship race)
        {
            var OldRace = Read(race.Id);
            OldRace.Year = race.Year;
            //OldRace.WCC = race.WCC;
            OldRace.NumberOfRaces = race.NumberOfRaces;
            db.SaveChanges();

        }
    }
}
