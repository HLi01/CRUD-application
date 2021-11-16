using J2RXEK_HFT_2021221.Models;
using System;
using System.Collections.Generic;

namespace J2RXEK_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //ChampionshipDBContext db = new ChampionshipDBContext();
            //db.SaveChanges();

            System.Threading.Thread.Sleep(8000);
            RestService rest = new RestService("http://localhost:65297");

            var drivers = rest.Get<Driver>("driver");
            var teams = rest.Get<Team>("team");


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
            Console.WriteLine("Select which method do you want to run. \n");
            Console.WriteLine("1 - Drivers with even number");
            Console.WriteLine("2 - Number of world champions");
            Console.WriteLine("3 - Sum of championships grouped by engines");
            Console.WriteLine("4 - Exist a driver which debuted in provided year, and won a race");
            Console.WriteLine("5 - Number of wins of the provided driver");
            Console.WriteLine("6 - Date of the provided race\n");

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
                        Console.Write("Give a race id: ");
                        string id = Console.ReadLine();
                        Console.WriteLine("Answer: " + rest.GetSingle<DateTime>("stat/racedate/"+id)); break;
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
            Console.WriteLine($"\nWhat would you like to do to {table}? \n");
            Console.WriteLine("1 - List.");
            Console.WriteLine("2 - Get a single element.");
            Console.WriteLine("3 - Create a new element.");
            Console.WriteLine("4 - Update an existing element.");
            Console.WriteLine("5 - Remove an existing element.");
            Console.WriteLine();
            
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
                        //Console.Write("Give a team: ");
                        //string team = Console.ReadLine();
                        Console.Write("Give a debut year: ");
                        string debut = Console.ReadLine();
                        Console.Write("Give if the driver is champion (true/false): ");
                        bool champ = bool.Parse(Console.ReadLine());
                        Driver newDriver = new Driver() { Name = name, Number = number, DebutYear = debut, IsChampion = champ };
                        rest.Post<Driver>(newDriver,"driver");
                        Console.WriteLine("Driver created successfully.");
                        break;
                    case 4:
                        Console.Write("Give a driver number: ");
                        int id4 = int.Parse(Console.ReadLine());
                        Console.Write("Give a name: ");
                        string name4 = Console.ReadLine();
                        //Console.Write("Give a team: ");
                        //string team4 = Console.ReadLine();
                        Console.Write("Give a debut year: ");
                        string debut4 = Console.ReadLine();
                        Console.Write("Give if the driver is champion (true/false): ");
                        bool champ4 = bool.Parse(Console.ReadLine());
                        Driver updatedDriver = new Driver() { Name = name4, Number = id4, DebutYear = debut4, IsChampion = champ4 };
                        rest.Put<Driver>(updatedDriver, "driver");
                        break;
                    case 5:
                        Console.Write("Give a driver id: ");
                        int id5 = int.Parse(Console.ReadLine());
                        rest.Delete(id5, "driver");
                        break;
                    default: throw new MenuException("Given response is not valid!");
                }
            }
            
        }
        public static int MainMenu()
        {
            Console.WriteLine("\nGreetings! Which table do you want to edit? ");
            Console.WriteLine($"1 - Driver");
            Console.WriteLine($"2 - Team");
            Console.WriteLine($"3 - Championship");
            Console.WriteLine($"4 - Other - Non-crud methods");
            Console.WriteLine($"0 - Exit");
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
