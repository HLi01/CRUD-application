using J2RXEK_HFT_2021221.Models;
using System.Linq;

namespace J2RXEK_HFT_2021221.Repository
{
    public interface IDriverRepository
    {
        void Create(Driver driver);
        void Update(Driver driver);
        Driver Read(int id);
        IQueryable<Driver> ReadAll();
        void Delete(int id);
    }
}
