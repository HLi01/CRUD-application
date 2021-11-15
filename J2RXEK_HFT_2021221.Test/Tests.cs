using NUnit.Framework;
using System;
using Moq;
using J2RXEK_HFT_2021221.Logic;
using J2RXEK_HFT_2021221.Repository;
using J2RXEK_HFT_2021221.Models;
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

            Driver LH = new Driver() { Name = "Lewis Hamilton", Number = 44, DebutYear = "2007", IsChampion = true };
            Driver SV = new Driver() { Name = "Sebastian Vettel", Number = 5, DebutYear = "2007", IsChampion = true };
            Driver FA = new Driver() { Name = "Fernando Alonso", Number = 14, DebutYear = "2001", IsChampion = true };
            Driver CL = new Driver() { Name = "Charles Leclerc", Number = 16, DebutYear = "2018", IsChampion = false };
            Driver CS = new Driver() { Name = "Carlos Sainz", Number = 55, DebutYear = "2015", IsChampion = false };
            Driver MV = new Driver() { Name = "Max Verstappen", Number = 33, DebutYear = "2014", IsChampion = false };
            Driver SP = new Driver() { Name = "Sergio Perez", Number = 11, DebutYear = "2011", IsChampion = false };

            Team Ferr = new Team() { TeamName = "Ferrari", TeamPrincipal = "Mattia Binotto", PowerUnit = "Ferrari", ChampionshipsWon = 16 };
            Team Merc = new Team() { TeamName = "Mercedes", TeamPrincipal = "Toto Wolff", PowerUnit = "Mercedes", ChampionshipsWon = 7 };
            Team Alfa = new Team() { TeamName = "Alfa Romeo", TeamPrincipal = "Frederic Vasseur", PowerUnit = "Ferrari", ChampionshipsWon = 5 };
            Team Alp = new Team() { TeamName = "Alpine", TeamPrincipal = "Marcin Budkowski", PowerUnit = "Renault", ChampionshipsWon = 2 };
            Team Ast = new Team() { TeamName = "Aston Martin", TeamPrincipal = "Otmar Szafnauer", PowerUnit = "Mercedes", ChampionshipsWon = 0 };

            Championship fourth = new Championship() { RaceID = "SPA_05_09", Location = "Spain", Date = DateTime.Parse("2021.05.09"), WinnerName = LH.Name };
            Championship fifth = new Championship() { RaceID = "MON_05_23", Location = "Monaco", Date = DateTime.Parse("2021.05.23"), WinnerName = MV.Name };
            Championship sixt = new Championship() { RaceID = "AZE_06_06", Location = "Azerbaijan", Date = DateTime.Parse("2021.06.06"), WinnerName = SP.Name };
            Championship seventh = new Championship() { RaceID = "FRA_06_20", Location = "France", Date = DateTime.Parse("2021.06.20"), WinnerName = MV.Name };
            
            mockDriverRepository.Setup((t) => t.ReadAll()).Returns(new List<Driver>() { SV, FA, CL, CS, SP, MV }.AsQueryable());
            mockTeamRepository.Setup((t) => t.ReadAll()).Returns(new List<Team>() { Ferr,Alp,Ast,Alfa,Merc }.AsQueryable());
            mockChampionshipRepository.Setup((t) => t.ReadAll()).Returns(new List<Championship>() {fourth, fifth, sixt, seventh}.AsQueryable());

            dl = new DriverLogic(mockDriverRepository.Object);
            tl = new TeamLogic(mockTeamRepository.Object);
            cl = new ChampionshipLogic(mockChampionshipRepository.Object,mockTeamRepository.Object,mockDriverRepository.Object);
        }
        [Test]
        public void CreateDriverNumberExceptionTest()
        {
            Assert.That(()=> { dl.Create(new Driver() { Name = "Pastor Malonado", Number = 105 }); }, Throws.ArgumentException);
        }
        [Test]
        public void CreateDriverNameExceptionTest()
        {
            Assert.That(() => { dl.Create(new Driver() { Name = "Guttierez" }); }, Throws.ArgumentException);
        }
        [Test]
        public void CreateTeamChampionshipsExceptionTest()
        {
            Assert.That(() => { tl.Create(new Team() { TeamName = "BMW", ChampionshipsWon=-3 }); }, Throws.ArgumentException);
        }
        [Test]
        public void NumberOfChampions()
        {
            var result = dl.NumberOfChampions();

            Assert.That(result, Is.EqualTo(2));
        }
        [TestCase("Charles Leclerc")]
        [TestCase("Fernando Alonso")]
        public void EvenNumbers(string name)
        {
            var result = dl.EvenNumbers();
            Assert.That(result.ToList(), Has.Exactly(1).Matches<Driver>(x=>x.Name==name));
        }
        [TestCase("Ferrari",21)]
        [TestCase("Renault",2)]
        [TestCase("Mercedes",7)]
        public void SumChampsByEngine(string team, int won)
        {
            var result = tl.SumChampByEngines();
            Assert.AreEqual(3,result.Count());
            Assert.IsTrue(result.Any(x=>x.Key==team && x.Value==won));
        }
        [TestCase("Max Verstappen",2)]
        public void Wins(string name, int win)
        {
            var result = cl.Wins(name);
            Assert.That(result, Is.EqualTo(win));
        }
        [TestCase("SPA_05_09", 2021,5,9)]
        [TestCase("FRA_06_20", 2021, 6, 20)]
        public void RaceDate(string id, int year, int month, int day)
        {
            var result = cl.RaceDate(id);
            Assert.That(result.Year, Is.EqualTo(year));
            Assert.That(result.Month, Is.EqualTo(month));
            Assert.That(result.Day, Is.EqualTo(day));
        }
        [Test]
        public void TeamReadAll()
        {
            Assert.That(() => { tl.ReadAll(); }, Is.Not.Null);
        }
        [TestCase("2001",false)]
        [TestCase("2010",false)]
        [TestCase("2014",true)]
        public void DebutedAndWon(string debutYear, bool won)
        {
            var result = cl.DebutedAndWon(debutYear);
            Assert.That(result, Is.EqualTo(won));
        }
    }
}
