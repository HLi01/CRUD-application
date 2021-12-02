using J2RXEK_HFT_2021221.Models;
using System.Collections.Generic;

namespace J2RXEK_HFT_2021221.Logic
{
    public interface IChampionshipLogic
    {
        void Create(Championship championship);
        Championship Read(int id);
        void Update(Championship championship);
        void Delete(int id);
        IEnumerable<Championship> ReadAll();
        int Wins(int id);
        string RaceNumber(int number);
        IEnumerable<KeyValuePair<string, int>> ChampsByTeam();
        IEnumerable<KeyValuePair<string, string>> FirstDriversOfTeams();
        IEnumerable<KeyValuePair<string, double>> AvgAgeByTeam();
        string WinnerTeamInGivenYear(int year);
    }
}
