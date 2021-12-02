using J2RXEK_HFT_2021221.Models;
using System.Linq;

namespace J2RXEK_HFT_2021221.Repository
{
    public interface IChampionshipRepository
    {
        void Create(Championship championship);
        void Update(Championship championship);
        Championship Read(int id);
        IQueryable<Championship> ReadAll();
        void Delete(int id);
    }
}
