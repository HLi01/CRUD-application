using J2RXEK_HFT_2021221.Models;
using System;
using System.Collections.Generic;

namespace J2RXEK_HFT_2021221.Client
{
    public delegate void ConsoleClear();
    public delegate string ConsoleInput();
    public delegate void ConsoleOutput(string word);

    public class Menu
    {
        public event ConsoleClear clear;
        public event ConsoleInput input;
        public event ConsoleOutput write;
        public event ConsoleOutput writeline;

        public void Start()
        {
            write?.Invoke("Loading");
            System.Threading.Thread.Sleep(1000);
            write?.Invoke(".");
            System.Threading.Thread.Sleep(1000);
            write?.Invoke(".");
            System.Threading.Thread.Sleep(1000);
            write?.Invoke(".");
            System.Threading.Thread.Sleep(1000);
            clear?.Invoke();
            MainMenu();
        }
        public void MainMenu()
        {
            string response = "";
            while (response != "0")
            {
                writeline?.Invoke("\nHello! Which table do you want to edit?\n");
                writeline?.Invoke($"1 - Driver");
                writeline?.Invoke($"2 - Team");
                writeline?.Invoke($"3 - Championship");
                writeline?.Invoke($"4 - Other - Non-crud methods");
                writeline?.Invoke($"0 - Exit");
                write?.Invoke("\nChoice: ");
                response = input?.Invoke();
                switch (response)
                {
                    case "1":
                        CRUDMenu("driver");
                        break;
                    case "2":
                        CRUDMenu("team");
                        break;
                    case "3":
                        CRUDMenu("championship");
                        break;
                    case "4":
                        NONCRUDMenu();
                        break;
                    case "0":
                        Environment.Exit(1);
                        break;
                    default:
                        writeline?.Invoke("Not a valid menu option!");
                        break;
                }
            }
        }
        public void NONCRUDMenu()
        {
            RestService rest = new RestService("http://localhost:65297");
            writeline?.Invoke("\nSelect which method do you want to run. \n");
            writeline?.Invoke("1 - Drivers with even number");
            writeline?.Invoke("2 - Number of world champions");
            writeline?.Invoke("3 - Number of wins of the provided team");
            writeline?.Invoke("4 - Which driver has the given number");
            writeline?.Invoke("5 - Number of champions by team");
            writeline?.Invoke("6 - #1 drivers of the teams");
            writeline?.Invoke("7 - Average age by teams");
            writeline?.Invoke("8 - Winner team in the given year");
            write?.Invoke("\nAnswer: ");
            string response = input?.Invoke();
            switch (response)
            {
                case "1":
                    var result1 = rest.Get<Driver>("stat/evennumbers");
                    foreach (var item in result1)
                    {
                        writeline?.Invoke("Name: " + item.Name + ", Number: " + item.Number);
                    }
                    break;
                case "2": writeline?.Invoke("Answer: " + rest.GetSingle<int>("stat/numberofchampions")); break;
                case "3":
                    write?.Invoke("Give a team id: ");
                    int id = int.Parse(input?.Invoke());
                    writeline?.Invoke("Answer: " + rest.GetSingle<int>("stat/wins/" + id)); break;
                case "4":
                    write?.Invoke("Give a number: ");
                    int racenumber = int.Parse(input?.Invoke());
                    writeline?.Invoke("Answer: " + rest.GetSingle<string>("stat/racenumber/" + racenumber)); break;
                case "5":
                    var result5 = rest.Get<KeyValuePair<string, int>>("stat/champsbyteam");
                    foreach (var item in result5)
                    {
                        writeline?.Invoke($"{item.Key} : {item.Value}");
                    }
                    break;
                case "6":
                    var result6 = rest.Get<KeyValuePair<string, string>>("stat/firstdriversofteams");
                    foreach (var item in result6)
                    {
                        writeline?.Invoke($"{item.Key} : {item.Value}");
                    }
                    break;
                case "7":
                    var result7 = rest.Get<KeyValuePair<string, double>>("stat/avgagebyteam");
                    foreach (var item in result7)
                    {
                        writeline?.Invoke($"{item.Key} : {item.Value}");
                    }
                    break;
                case "8":
                    write?.Invoke("Give a year: ");
                    int year = int.Parse(input?.Invoke());
                    writeline?.Invoke("Answer: " + rest.GetSingle<string>("stat/winnerteamingivenyear/" + year));
                    break;
                default: writeline?.Invoke("Not a valid menu option!"); break;
            }
        }

        public void CRUDMenu(string table)
        {
            RestService rest = new RestService("http://localhost:65297");
            writeline?.Invoke($"\nWhat would you like to do to {table} table? \n");
            writeline?.Invoke("1 - List.");
            writeline?.Invoke("2 - Get a single element.");
            writeline?.Invoke("3 - Create a new element.");
            writeline?.Invoke("4 - Update an existing element.");
            writeline?.Invoke("5 - Remove an existing element.");
            write?.Invoke("\nChoice: ");

            int response = int.Parse(input?.Invoke());
            if (table == "driver")
            {
                switch (response)
                {
                    case 1:
                        var result1 = rest.Get<Driver>("driver");
                        foreach (var item in result1)
                        {
                            writeline?.Invoke($"Name: {item.Name} ({item.Number}), Age:{item.Age}, Debut: {item.DebutYear}, Champion: {item.IsChampion}");
                        }
                        break;
                    case 2:
                        write?.Invoke("Give a driver id (number): ");
                        var result2 = rest.GetSingle<Driver>("driver" + "/" + input?.Invoke());
                        writeline?.Invoke($"Name: {result2.Name} ({result2.Number}), Age:{result2.Age}, Debut: {result2.DebutYear}, Champion: {result2.IsChampion}");
                        break;
                    case 3:
                        write?.Invoke("Give a driver name: ");
                        string name = input?.Invoke();
                        write?.Invoke("Give a driver number: ");
                        int number = int.Parse(input?.Invoke());
                        write?.Invoke("Give a driver age: ");
                        int age = int.Parse(input?.Invoke());
                        write?.Invoke("Give a debut year: ");
                        string debut = input?.Invoke();
                        write?.Invoke("Give a team Id: ");
                        int teamid = int.Parse(input?.Invoke());
                        write?.Invoke("Give if the driver is champion (true/false): ");
                        bool champ = bool.Parse(input?.Invoke());
                        Driver newDriver = new Driver() { Name = name, Number = number, Age = age, DebutYear = debut, IsChampion = champ, TeamId = teamid };
                        rest.Post<Driver>(newDriver, "driver");
                        writeline?.Invoke("\nDriver created successfully.");
                        break;
                    case 4:
                        write?.Invoke("Give a driver id: ");
                        int id4 = int.Parse(input?.Invoke());
                        write?.Invoke("Give a name: ");
                        string name4 = input?.Invoke();
                        write?.Invoke("Give a number: ");
                        int number4 = int.Parse(input?.Invoke());
                        write?.Invoke("Give a driver age: ");
                        int age4 = int.Parse(input?.Invoke());
                        write?.Invoke("Give a debut year: ");
                        string debut4 = input?.Invoke();
                        write?.Invoke("Give if the driver is champion (true/false): ");
                        bool champ4 = bool.Parse(input?.Invoke());
                        write?.Invoke("Give a team Id: ");
                        int teamid4 = int.Parse(input?.Invoke());
                        Driver updatedDriver = new Driver() { Id = id4, Name = name4, Age = age4, Number = number4, DebutYear = debut4, IsChampion = champ4, TeamId = teamid4 };
                        rest.Put<Driver>(updatedDriver, "driver");
                        writeline?.Invoke("\nDriver updated successfully.");
                        break;
                    case 5:
                        write?.Invoke("Give a driver id: ");
                        int id5 = int.Parse(input?.Invoke());
                        rest.Delete(id5, "driver");
                        writeline?.Invoke("\nDriver deleted successfully.");
                        break;
                    default: writeline?.Invoke("Not a valid menu option!"); break;
                }
            }
            else if (table == "team")
            {
                switch (response)
                {
                    case 1:
                        var result1 = rest.Get<Team>("team");
                        foreach (var item in result1)
                        {
                            writeline?.Invoke($"Team name: {item.TeamName} ({item.Id}), Power unit: {item.PowerUnit}, Principal: {item.TeamPrincipal}, Championships won: {item.ChampionshipsWon}");
                        }
                        break;
                    case 2:
                        write?.Invoke("Give a team id (number): ");
                        var result2 = rest.GetSingle<Team>("team/" + input?.Invoke());
                        writeline?.Invoke($"Team name: {result2.TeamName} ({result2.Id}), Power unit: {result2.PowerUnit}, Principal: {result2.TeamPrincipal}, Championships won: {result2.ChampionshipsWon}");
                        break;
                    case 3:
                        write?.Invoke("Give a team name: ");
                        string name = input?.Invoke();
                        write?.Invoke("Give the power unit: ");
                        string power = input?.Invoke();
                        write?.Invoke("Give the name of principal: ");
                        string principal = input?.Invoke();
                        write?.Invoke("Give a number of championship win: ");
                        int champwin = int.Parse(input?.Invoke());
                        Team newteam = new Team() { TeamName = name, PowerUnit = power, TeamPrincipal = principal, ChampionshipsWon = champwin };
                        rest.Post<Team>(newteam, "team");
                        writeline?.Invoke("\nTeam created successfully.");
                        break;
                    case 4:
                        write?.Invoke("Give a team id: ");
                        int id4 = int.Parse(input?.Invoke());
                        write?.Invoke("Give a team name: ");
                        string name4 = input?.Invoke();
                        write?.Invoke("Give the power unit: ");
                        string power4 = input?.Invoke();
                        write?.Invoke("Give the name of principal: ");
                        string principal4 = input?.Invoke();
                        write?.Invoke("Give a number of championship win: ");
                        int champwin4 = int.Parse(input?.Invoke());
                        Team updateteam = new Team() { Id = id4, TeamName = name4, PowerUnit = power4, TeamPrincipal = principal4, ChampionshipsWon = champwin4 };
                        rest.Put<Team>(updateteam, "team");
                        writeline?.Invoke("\nTeam updated successfully.");
                        break;
                    case 5:
                        write?.Invoke("Give a team id: ");
                        int id5 = int.Parse(input?.Invoke());
                        rest.Delete(id5, "team");
                        writeline?.Invoke("\nTeam deleted successfully.");
                        break;
                    default: writeline?.Invoke("Not a valid menu option!"); break;
                }
            }
            else
            {
                switch (response)
                {
                    case 1:
                        var result1 = rest.Get<Championship>("championship");
                        foreach (var item in result1)
                        {
                            writeline?.Invoke($"Championship year: {item.Year} ({item.Id}), Number of races: {item.NumberOfRaces}, WCC id: {item.WCC}");
                        }
                        break;
                    case 2:
                        write?.Invoke("Give a championship id (number): ");
                        var result2 = rest.GetSingle<Championship>("championship/" + input?.Invoke());
                        writeline?.Invoke($"Championship year: {result2.Year} ({result2.Id}), Number of races: {result2.NumberOfRaces}, WCC id: {result2.WCC}");
                        break;
                    case 3:
                        write?.Invoke("Give the year: ");
                        int year = int.Parse(input?.Invoke());
                        write?.Invoke("Give the namber of races: ");
                        int races = int.Parse(input?.Invoke());
                        write?.Invoke("Give the WCC id: ");
                        int wcc = int.Parse(input?.Invoke());
                        Championship newchampionship = new Championship() { Year = year, NumberOfRaces = races, WCC = wcc };
                        rest.Post<Championship>(newchampionship, "championship");
                        writeline?.Invoke("\nChampionship created successfully.");
                        break;
                    case 4:
                        write?.Invoke("Give the championship id: ");
                        int id4 = int.Parse(input?.Invoke());
                        write?.Invoke("Give the year: ");
                        int year4 = int.Parse(input?.Invoke());
                        write?.Invoke("Give the namber of races: ");
                        int races4 = int.Parse(input?.Invoke());
                        write?.Invoke("Give the WCC id: ");
                        int wcc4 = int.Parse(input?.Invoke());
                        Championship updatedchampionship = new Championship() { Id = id4, Year = year4, NumberOfRaces = races4, WCC = wcc4 };
                        rest.Put<Championship>(updatedchampionship, "championship");
                        writeline?.Invoke("\nChampionship updated successfully.");
                        break;
                    case 5:
                        write?.Invoke("Give a championship id: ");
                        int id5 = int.Parse(input?.Invoke());
                        rest.Delete(id5, "championship");
                        writeline?.Invoke("\nChampionship deleted successfully.");
                        break;
                    default: writeline?.Invoke("Not a valid menu option!"); break;
                }
            }
        }
    }
}

