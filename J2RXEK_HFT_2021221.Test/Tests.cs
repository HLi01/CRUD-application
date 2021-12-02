using J2RXEK_HFT_2021221.Logic;
using J2RXEK_HFT_2021221.Models;
using J2RXEK_HFT_2021221.Repository;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace J2RXEK_HFT_2021221.Test
{
    [TestFixture]
    public class Tests
    {
        DriverLogic dl;
        TeamLogic tl;
        ChampionshipLogic cl;

        [SetUp]
        public void Init()
        {
            var mockDriverRepository = new Mock<IDriverRepository>();
            var mockTeamRepository = new Mock<ITeamRepository>();
            var mockChampionshipRepository = new Mock<IChampionshipRepository>();

            Team Ferr = new Team() { Id = 2, TeamName = "Ferrari", TeamPrincipal = "Mattia Binotto", PowerUnit = "Ferrari", ChampionshipsWon = 16 };
            Team Merc = new Team() { Id = 1, TeamName = "Mercedes", TeamPrincipal = "Toto Wolff", PowerUnit = "Mercedes", ChampionshipsWon = 7 };
            Team RedB = new Team() { Id = 3, TeamName = "Red Bull", TeamPrincipal = "Christian Horner", PowerUnit = "Honda", ChampionshipsWon = 4 };
            Team Alp = new Team() { Id = 5, TeamName = "Alpine", TeamPrincipal = "Marcin Budkowski", PowerUnit = "Renault", ChampionshipsWon = 2 };
            Team Ast = new Team() { Id = 6, TeamName = "Aston Martin", TeamPrincipal = "Otmar Szafnauer", PowerUnit = "Mercedes", ChampionshipsWon = 0 };

            Driver LH = new Driver() { Id = 1, Name = "Lewis Hamilton", Number = 44, Age = 36, DebutYear = "2007", IsChampion = true, TeamId = 1, Team = Merc };
            Driver VB = new Driver() { Id = 2, Name = "Valtteri Bottas", Number = 77, Age = 32, DebutYear = "2013", IsChampion = false, TeamId = 1, Team = Merc };
            Driver SV = new Driver() { Id = 5, Name = "Sebastian Vettel", Number = 5, Age = 34, DebutYear = "2007", IsChampion = true, TeamId = 6, Team = Ast };
            Driver LS = new Driver() { Id = 6, Name = "Lance Stroll", Number = 18, Age = 23, DebutYear = "2017", IsChampion = false, TeamId = 6, Team = Ast };
            Driver FA = new Driver() { Id = 15, Name = "Fernando Alonso", Number = 14, Age = 40, DebutYear = "2001", IsChampion = true, TeamId = 5, Team = Alp };
            Driver EO = new Driver() { Id = 16, Name = "Esteban Ocon", Number = 31, Age = 25, DebutYear = "2016", IsChampion = false, TeamId = 5, Team = Alp };
            Driver CL = new Driver() { Id = 7, Name = "Charles Leclerc", Number = 16, Age = 24, DebutYear = "2018", IsChampion = false, TeamId = 2, Team = Ferr };
            Driver CS = new Driver() { Id = 8, Name = "Carlos Sainz", Number = 55, Age = 27, DebutYear = "2015", IsChampion = false, TeamId = 2, Team = Ferr };
            Driver MV = new Driver() { Id = 3, Name = "Max Verstappen", Number = 33, Age = 24, DebutYear = "2014", IsChampion = false, TeamId = 3, Team = RedB };
            Driver SP = new Driver() { Id = 4, Name = "Sergio Perez", Number = 11, Age = 31, DebutYear = "2011", IsChampion = false, TeamId = 3, Team = RedB };

            Championship first = new Championship() { Id = 1, Year = 2008, WCC = Ferr.Id, NumberOfRaces = 18, Team = Ferr };
            Championship second = new Championship() { Id = 2, Year = 2001, WCC = Ferr.Id, NumberOfRaces = 17, Team = Ferr };
            Championship fourth = new Championship() { Id = 4, Year = 2011, WCC = RedB.Id, NumberOfRaces = 19, Team = RedB };
            Championship fifth = new Championship() { Id = 5, Year = 2012, WCC = RedB.Id, NumberOfRaces = 20, Team = RedB };
            Championship sixt = new Championship() { Id = 6, Year = 2013, WCC = RedB.Id, NumberOfRaces = 19, Team = RedB };
            Championship seventh = new Championship() { Id = 7, Year = 2014, WCC = Merc.Id, NumberOfRaces = 19, Team = Merc };

            Ferr.Drivers.Add(CS);
            Ferr.Drivers.Add(CL);
            Ferr.Championships.Add(first);
            Ferr.Championships.Add(second);
            Merc.Drivers.Add(LH);
            Merc.Drivers.Add(VB);
            Merc.Championships.Add(seventh);
            RedB.Drivers.Add(MV);
            RedB.Drivers.Add(SP);
            RedB.Championships.Add(fourth);
            RedB.Championships.Add(fifth);
            RedB.Championships.Add(sixt);
            Alp.Drivers.Add(FA);
            Alp.Drivers.Add(EO);
            Ast.Drivers.Add(SV);
            Ast.Drivers.Add(LS);

            mockDriverRepository.Setup((t) => t.ReadAll()).Returns(new List<Driver>() { LH, VB, SV, LS, FA, EO, CL, CS, SP, MV }.AsQueryable());
            mockTeamRepository.Setup((t) => t.ReadAll()).Returns(new List<Team>() { Ferr, Alp, Ast, Merc, RedB }.AsQueryable());
            mockChampionshipRepository.Setup((t) => t.ReadAll()).Returns(new List<Championship>() { first, second, fourth, fifth, sixt, seventh }.AsQueryable());

            dl = new DriverLogic(mockDriverRepository.Object);
            tl = new TeamLogic(mockTeamRepository.Object);
            cl = new ChampionshipLogic(mockChampionshipRepository.Object, mockTeamRepository.Object);
        }
        [Test]
        public void CreateDriverNumberExceptionTest()
        {
            Assert.That(() => { dl.Create(new Driver() { Name = "Pastor Malonado", Number = 105 }); }, Throws.ArgumentException);
        }
        [Test]
        public void CreateDriverNameExceptionTest()
        {
            Assert.That(() => { dl.Create(new Driver() { Name = "Guttierez" }); }, Throws.ArgumentException);
        }
        [Test]
        public void CreateTeamChampionshipsExceptionTest()
        {
            Assert.That(() => { tl.Create(new Team() { TeamName = "BMW", ChampionshipsWon = -3 }); }, Throws.ArgumentException);
        }
        [Test]
        public void NumberOfChampions()
        {
            var result = dl.NumberOfChampions();

            Assert.That(result, Is.EqualTo(3));
        }

        [TestCase("Charles Leclerc")]
        [TestCase("Lewis Hamilton")]
        public void EvenNumbers(string name)
        {
            var result = dl.EvenNumbers();
            Assert.That(result.ToList(), Has.Exactly(1).Matches<Driver>(x => x.Name == name));
        }

        [TestCase(2, 2)]
        [TestCase(1, 1)]
        public void Wins(int id, int number)
        {
            var result = cl.Wins(id);
            Assert.That(result, Is.EqualTo(number));
        }

        [Test]
        public void TeamReadAll()
        {
            Assert.That(() => { tl.ReadAll(); }, Is.Not.Null);
        }

        [Test]
        public void ChampsByTeam()
        {
            var result = cl.ChampsByTeam();
            var teamschamp = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("Ferrari",0),
                new KeyValuePair<string, int>("Alpine",1),
                new KeyValuePair<string, int>("Aston Martin",1),
                new KeyValuePair<string, int>("Mercedes",1),
                new KeyValuePair<string, int>("Red Bull",0)
            };
            Assert.That(result, Is.EqualTo(teamschamp));
        }
        [Test]
        public void FirstDriversOfTeams()
        {
            var result = cl.FirstDriversOfTeams();
            var firstdriver = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Ferrari","Carlos Sainz"),
                new KeyValuePair<string, string>("Alpine","Fernando Alonso"),
                new KeyValuePair<string, string>("Aston Martin","Sebastian Vettel"),
                new KeyValuePair<string, string>("Mercedes","Lewis Hamilton"),
                new KeyValuePair<string, string>("Red Bull","Sergio Perez")
            };
            Assert.That(result, Is.EqualTo(firstdriver));
        }
        [Test]
        public void AvgAgeByTeam()
        {
            var result = cl.AvgAgeByTeam();
            var avgage = new List<KeyValuePair<string, double>>()
            {
                new KeyValuePair<string, double>("Ferrari",25.5),
                new KeyValuePair<string, double>("Alpine",32.5),
                new KeyValuePair<string, double>("Aston Martin",28.5),
                new KeyValuePair<string, double>("Mercedes",34.0),
                new KeyValuePair<string, double>("Red Bull",27.5)
            };
            Assert.That(result, Is.EqualTo(avgage));
        }
        [TestCase(5, "Sebastian Vettel")]
        [TestCase(77, "Valtteri Bottas")]
        [TestCase(33, "Max Verstappen")]
        public void RaceNumber(int number, string name)
        {
            var result = cl.RaceNumber(number);
            Assert.That(result, Is.EqualTo(name));
        }

        [TestCase(2008, "Ferrari")]
        [TestCase(2012, "Red Bull")]
        [TestCase(2014, "Mercedes")]
        public void WinnerTeamInGivenYear(int year, string team)
        {
            var result = cl.WinnerTeamInGivenYear(year);
            Assert.That(result, Is.EqualTo(team));
        }

    }
}
