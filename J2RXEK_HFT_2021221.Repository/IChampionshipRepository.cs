using J2RXEK_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J2RXEK_HFT_2021221.Repository
{
    public interface IChampionshipRepository
    {
        void Create(Championship championship);
        void Update(Championship championship);
        Championship Read(string RaceID);
        IQueryable<Championship> ReadAll();
        void Delete(string RaceID);
    }
}
