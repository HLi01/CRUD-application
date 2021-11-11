using J2RXEK_HFT_2021221.Models;
using J2RXEK_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J2RXEK_HFT_2021221.Logic
{
    public class DriverLogic : IDriverLogics
    {
        IDriverRepository driverRepo;
        public DriverLogic(IDriverRepository driverRepo)
        {
            this.driverRepo = driverRepo;
        }
        public void Create(Driver driver)
        {
            if (driver.Number <1 || driver.Number>99)
            {
                throw new ArgumentException("The driver's number must be between 1 and 99!");
            }
            if (!(driver.Name.Trim().Contains(" ")))
            {
                throw new ArgumentException("The driver must have a first name and a surname!");
            }
            driverRepo.Create(driver);
        }

        public void Delete(int id)
        {
            driverRepo.Delete(id);
        }

        public Driver Read(int id)
        {
            return driverRepo.Read(id);
        }

        public IEnumerable<Driver> ReadAll()
        {
            return driverRepo.ReadAll();
        }

        public void Update(Driver driver)
        {
            driverRepo.Update(driver);
        }

        //non-crud - which drivers have even number?
        public IEnumerable<Driver> EvenNumbers()
        {
            IEnumerable<Driver> drivers = driverRepo.ReadAll().Where(x => x.Number % 2 == 0);
            return drivers;
        }
        //how many champions are there currently? 
        public int NumberOfChampions()
        {
            return driverRepo.ReadAll().Where(x => x.IsChampion == true).Count();
        }
    }
}
