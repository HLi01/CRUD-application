using J2RXEK_HFT_2021221.Data;
using J2RXEK_HFT_2021221.Models;
using System.Linq;

namespace J2RXEK_HFT_2021221.Repository
{
    public class DriverRepository : IDriverRepository
    {
        ChampionshipDBContext db;
        public DriverRepository(ChampionshipDBContext db)
        {
            this.db = db;
        }
        public void Create(Driver driver)
        {
            db.Drivers.Add(driver);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var DriverToDelete = Read(id);
            db.Drivers.Remove(DriverToDelete);
            db.SaveChanges();
        }

        public Driver Read(int id)
        {
            return db.Drivers.FirstOrDefault(t => t.Id == id);
        }

        public IQueryable<Driver> ReadAll()
        {
            return db.Drivers;
        }

        public void Update(Driver driver)
        {
            var OldDriver = Read(driver.Id);
            OldDriver.Name = driver.Name;
            OldDriver.Number = driver.Number;
            OldDriver.Age = driver.Age;
            OldDriver.DebutYear = driver.DebutYear;
            OldDriver.IsChampion = driver.IsChampion;
            db.SaveChanges();
        }
    }
}
