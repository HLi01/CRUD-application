﻿using J2RXEK_HFT_2021221.Data;
using J2RXEK_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Delete(int Number)
        {
            var DriverToDelete = Read(Number);
            db.Drivers.Remove(DriverToDelete);
            db.SaveChanges();
        }

        public Driver Read(int Number)
        {
            return db.Drivers.FirstOrDefault(t => t.Number == Number);
        }

        public IQueryable<Driver> ReadAll()
        {
            return db.Drivers;
        }

        public void Update(Driver driver)
        {
            var OldDriver = Read(driver.Number);
            OldDriver.Name = driver.Name;
            OldDriver.DebutYear = driver.DebutYear;
            OldDriver.IsChampion = driver.IsChampion;
            db.SaveChanges();
        }
    }
}