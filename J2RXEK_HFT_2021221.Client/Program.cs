using System;
using J2RXEK_HFT_2021221.Data;

namespace J2RXEK_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ChampionshipDBContext db = new ChampionshipDBContext();
            db.SaveChanges();
        }
    }
}
