using J2RXEK_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J2RXEK_HFT_2021221.Repository
{
    public interface IDriverRepository
    {
        void Create(Driver driver);
        void Update(Driver driver);
        Driver Read(int Number);
        IQueryable<Driver> ReadAll();
        void Delete(int Number);
    }
}
