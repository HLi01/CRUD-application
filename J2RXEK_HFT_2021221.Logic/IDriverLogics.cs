using J2RXEK_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J2RXEK_HFT_2021221.Logic
{
    interface IDriverLogics
    {
        void Create(Driver driver);
        Driver Read(int id);
        void Update(Driver driver);
        void Delete(int id);
        IEnumerable<Driver> ReadAll();
    }
}
