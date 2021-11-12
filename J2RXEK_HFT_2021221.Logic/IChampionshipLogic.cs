using J2RXEK_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J2RXEK_HFT_2021221.Logic
{
    public interface IChampionshipLogic
    {
        void Create(Championship championship);
        Championship Read(string id);
        void Update(Championship championship);
        void Delete(string id);
        IEnumerable<Championship> ReadAll();
    }
}
