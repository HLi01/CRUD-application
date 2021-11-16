using J2RXEK_HFT_2021221.Models;
using System;
using System.Collections.Generic;

namespace J2RXEK_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Loading...");
            System.Threading.Thread.Sleep(7000);
            Console.Clear();
            RestService rest = new RestService("http://localhost:65297");

            bool quit=false;
            while (quit==false)
            {
                try
                {
                    int main=MainMenu();
                    if (main==0)
                    {
                        quit = true;
                    }
                    switch (main)
                    {
                        case 1: CRUDMenu("driver");break;
                        case 2: CRUDMenu("team");break;
                        case 3: CRUDMenu("championship");break;
                        case 4: NONCRUDMenu();break;
                        default: Environment.Exit(1); break;
                    }
                }
                catch (MenuException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        static void NONCRUDMenu()
        {
            RestService rest = new RestService("http://localhost:65297");
            Console.WriteLine("\nSelect which method do you want to run. \n");
            Console.WriteLine("1 - Drivers with even number");
            Console.WriteLine("2 - Number of world champions");
            Console.WriteLine("3 - Sum of championships grouped by engines");
            Console.WriteLine("4 - Exist a driver which debuted in provided year, and won a race");
            Console.WriteLine("5 - Number of wins of the provided driver");
            Console.WriteLine("6 - Date of the provided race");
            Console.Write("\nAnswer: ");

            int response = int.Parse(Console.ReadLine());
            if (response >= 1 && response <= 6)
            {
                switch (response)
                {
                    case 1:
                        var result1 = rest.Get<Driver>("stat/evennumbers");
                        foreach (var item in result1)
                        {
                            Console.WriteLine("Name: "+item.Name+", Number: "+item.Number);
                        }
                        break;
                    case 2: Console.WriteLine("Answer: " + rest.GetSingle<int>("stat/numberofchampions")); break;
                    case 3:
                        var result3 = rest.Get<KeyValuePair<string, int>>("stat/sumchampbyengines");
                        foreach (var item in result3)
                        {
                            Console.WriteLine("Engine: "+item.Key+", Championships: "+item.Value);
                        }
                        break;
                    case 4:
                        Console.Write("Give a debut year: ");
                        int year = int.Parse(Console.ReadLine());
                        Console.WriteLine("Answer: "+rest.GetSingle<bool>("stat/debutedandwon/"+year)); break;
                    case 5:
                        Console.Write("Give a driver name: ");
                        string name = Console.ReadLine();
                        Console.WriteLine("Answer: " + rest.GetSingle<int>("stat/wins/"+name.Trim())); break;
                    case 6:
                        Console.Write("Give the number of races: ");
                        int num = int.Parse(Console.ReadLine());
                        var result6 = rest.Get<Championship>("stat/racenumbers/" + num);
                        foreach (var item in result6)
                        {
                            Console.WriteLine($"\n Race Id: {item.Id}, Year: {item.Year}, WCC ID: {item.Id}, Number of races: {item.NumberOfRaces}");
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                throw new MenuException("Given response is not valid!");
            }
        }

        static void CRUDMenu(string table)
        {
            RestService rest = new RestService("http://localhost:65297");
            Console.WriteLine($"\nWhat would you like to do to {table} table? \n");
            Console.WriteLine("1 - List.");
            Console.WriteLine("2 - Get a single element.");
            Console.WriteLine("3 - Create a new element.");
            Console.WriteLine("4 - Update an existing element.");
            Console.WriteLine("5 - Remove an existing element.");
            Console.Write("\nAnswer: ");
            
            int response = int.Parse(Console.ReadLine());
            if (table=="driver")
            {
                switch (response)
                {
                    case 1:
                        var result1 = rest.Get<Driver>("driver");
                        foreach (var item in result1)
                        {
                            Console.WriteLine($"Name: {item.Name} ({item.Number}), Debut: {item.DebutYear}, Champion: {item.IsChampion}");
                        }
                        break;
                    case 2:
                        Console.Write("Give a driver id (number): ");
                        var result2 = rest.GetSingle<Driver>("driver" + "/" + Console.ReadLine());
                        Console.WriteLine($"Name: {result2.Name} ({result2.Number}), Debut: {result2.DebutYear}, Champion: {result2.IsChampion}");
                        break;
                    case 3:
                        Console.Write("Give a driver name: ");
                        string name = Console.ReadLine();
                        Console.Write("Give a driver number: ");
                        int number = int.Parse(Console.ReadLine());
                        Console.Write("Give a debut year: ");
                        string debut = Console.ReadLine();
                        Console.Write("Give a team Id: ");
                        int teamid = int.Parse(Console.ReadLine());
                        Console.Write("Give if the driver is champion (true/false): ");
                        bool champ = bool.Parse(Console.ReadLine());
                        Driver newDriver = new Driver() { Name = name, Number = number, DebutYear = debut, IsChampion = champ, TeamId=teamid };
                        rest.Post<Driver>(newDriver,"driver");
                        Console.WriteLine("\nDriver created successfully.");
                        break;
                    case 4:
                        Console.Write("Give a driver id: ");
                        int id4 = int.Parse(Console.ReadLine());
                        Console.Write("Give a name: ");
                        string name4 = Console.ReadLine();
                        Console.Write("Give a number: ");
                        int number4 = int.Parse(Console.ReadLine());
                        Console.Write("Give a debut year: ");
                        string debut4 = Console.ReadLine();
                        Console.Write("Give if the driver is champion (true/false): ");
                        bool champ4 = bool.Parse(Console.ReadLine());
                        Console.Write("Give a team Id: ");
                        int teamid4 = int.Parse(Console.ReadLine());
                        Driver updatedDriver = new Driver() { Id=id4, Name = name4, Number = number4, DebutYear = debut4, IsChampion = champ4, TeamId=teamid4 };
                        rest.Put<Driver>(updatedDriver, "driver");
                        Console.WriteLine("\nDriver updated successfully.");
                        break;
                    case 5:
                        Console.Write("Give a driver id: ");
                        int id5 = int.Parse(Console.ReadLine());
                        rest.Delete(id5, "driver");
                        Console.WriteLine("\nDriver deleted successfully.");
                        break;
                    default: throw new MenuException("Given response is not valid!");
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
                            Console.WriteLine($"Team name: {item.TeamName} ({item.Id}), Power unit: {item.PowerUnit}, Principal: {item.TeamPrincipal}, Championships won: {item.ChampionshipsWon}");
                        }
                        break;
                    case 2:
                        Console.Write("Give a team id (number): ");
                        var result2 = rest.GetSingle<Team>("team/" + Console.ReadLine());
                        Console.WriteLine($"Team name: {result2.TeamName} ({result2.Id}), Power unit: {result2.PowerUnit}, Principal: {result2.TeamPrincipal}, Championships won: {result2.ChampionshipsWon}");
                        break;
                    case 3:
                        Console.Write("Give a team name: ");
                        string name = Console.ReadLine();
                        Console.Write("Give the power unit: ");
                        string power = Console.ReadLine();
                        Console.Write("Give the name of principal: ");
                        string principal = Console.ReadLine();
                        Console.Write("Give a number of championship win: ");
                        int champwin = int.Parse(Console.ReadLine());
                        Team newteam = new Team() { TeamName=name, PowerUnit=power, TeamPrincipal=principal, ChampionshipsWon=champwin };
                        rest.Post<Team>(newteam, "team");
                        Console.WriteLine("\nTeam created successfully.");
                        break;
                    case 4:
                        Console.Write("Give a team id: ");
                        int id4 = int.Parse(Console.ReadLine());
                        Console.Write("Give a team name: ");
                        string name4 = Console.ReadLine();
                        Console.Write("Give the power unit: ");
                        string power4 = Console.ReadLine();
                        Console.Write("Give the name of principal: ");
                        string principal4 = Console.ReadLine();
                        Console.Write("Give a number of championship win: ");
                        int champwin4 = int.Parse(Console.ReadLine());
                        Team updateteam = new Team() { Id=id4, TeamName = name4, PowerUnit = power4, TeamPrincipal = principal4, ChampionshipsWon = champwin4 };
                        rest.Put<Team>(updateteam, "team");
                        Console.WriteLine("\nTeam updated successfully.");
                        break;
                    case 5:
                        Console.Write("Give a team id: ");
                        int id5 = int.Parse(Console.ReadLine());
                        rest.Delete(id5, "team");
                        Console.WriteLine("\nTeam deleted successfully.");
                        break;
                    default: throw new MenuException("Given response is not valid!");
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
                            Console.WriteLine($"Championship year: {item.Year} ({item.Id}), Number of races: {item.NumberOfRaces}, WCC id: {item.WCC}");
                        }
                        break;
                    case 2:
                        Console.Write("Give a championship id (number): ");
                        var result2 = rest.GetSingle<Championship>("championship/" + Console.ReadLine());
                        Console.WriteLine($"Championship year: {result2.Year} ({result2.Id}), Number of races: {result2.NumberOfRaces}, WCC id: {result2.WCC}");
                        break;
                    case 3:
                        Console.Write("Give the year: ");
                        int year = int.Parse(Console.ReadLine());
                        Console.Write("Give the namber of races: ");
                        int races = int.Parse(Console.ReadLine());
                        Console.Write("Give the WCC id: ");
                        int wcc = int.Parse(Console.ReadLine());
                        Championship newchampionship = new Championship() { Year=year, NumberOfRaces=races,WCC=wcc };
                        rest.Post<Championship>(newchampionship, "championship");
                        Console.WriteLine("\nChampionship created successfully.");
                        break;
                    case 4:
                        Console.Write("Give the championship id: ");
                        int id4 = int.Parse(Console.ReadLine());
                        Console.Write("Give the year: ");
                        int year4 = int.Parse(Console.ReadLine());
                        Console.Write("Give the namber of races: ");
                        int races4 = int.Parse(Console.ReadLine());
                        Console.Write("Give the WCC id: ");
                        int wcc4 = int.Parse(Console.ReadLine());
                        Championship updatedchampionship = new Championship() { Id=id4, Year = year4, NumberOfRaces = races4, WCC = wcc4 };
                        rest.Put<Championship>(updatedchampionship, "championship");
                        Console.WriteLine("\nChampionship updated successfully.");
                        break;
                    case 5:
                        Console.Write("Give a championship id: ");
                        int id5 = int.Parse(Console.ReadLine());
                        rest.Delete(id5, "championship");
                        Console.WriteLine("\nChampionship deleted successfully.");
                        break;
                    default: throw new MenuException("Given response is not valid!");
                }
            }
            
        }
        public static int MainMenu()
        {
            Console.WriteLine("\nGreetings! Which table do you want to edit?\n");
            Console.WriteLine($"1 - Driver");
            Console.WriteLine($"2 - Team");
            Console.WriteLine($"3 - Championship");
            Console.WriteLine($"4 - Other - Non-crud methods");
            Console.WriteLine($"0 - Exit");
            Console.Write("\nAnswer: ");
            int response = int.Parse(Console.ReadLine());
            if (response >=0 && response <=4)
            {
                return response;
            }
            throw new MenuException("Given response is not valid!");
        }
        public class MenuException : Exception
        {
            public MenuException(string message) : base(message) { }
        }

    }
}
