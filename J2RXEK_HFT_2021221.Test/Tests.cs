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

            Team Ferr = new Team() { Id = 2, TeamName = "Ferrari", TeamPrincipal = "Mattia Binotto", PowerUnit = "Ferrari", ChampionshipsWon = 16 };
            Team Merc = new Team() { Id = 1, TeamName = "Mercedes", TeamPrincipal = "Toto Wolff", PowerUnit = "Mercedes", ChampionshipsWon = 7 };
            Team RedB = new Team() { Id = 3, TeamName = "Red Bull", TeamPrincipal = "Christian Horner", PowerUnit = "Honda", ChampionshipsWon = 4 };
            Team Alfa = new Team() { Id = 7, TeamName = "Alfa Romeo", TeamPrincipal = "Frederic Vasseur", PowerUnit = "Ferrari", ChampionshipsWon = 5 };
            Team Alp = new Team() { Id = 5, TeamName = "Alpine", TeamPrincipal = "Marcin Budkowski", PowerUnit = "Renault", ChampionshipsWon = 2 };
            Team Ast = new Team() { Id = 6, TeamName = "Aston Martin", TeamPrincipal = "Otmar Szafnauer", PowerUnit = "Mercedes", ChampionshipsWon = 0 };

            Driver LH = new Driver() { Id = 1, Name = "Lewis Hamilton", Number = 44, DebutYear = "2007", IsChampion = true, TeamId = 1 };
            Driver SV = new Driver() { Id = 5, Name = "Sebastian Vettel", Number = 5, DebutYear = "2007", IsChampion = true, TeamId = 6 };
            Driver FA = new Driver() { Id = 15, Name = "Fernando Alonso", Number = 14, DebutYear = "2001", IsChampion = true, TeamId = 5 };
            Driver CL = new Driver() { Id = 7, Name = "Charles Leclerc", Number = 16, DebutYear = "2018", IsChampion = false, TeamId = 2 };
            Driver CS = new Driver() { Id = 8, Name = "Carlos Sainz", Number = 55, DebutYear = "2015", IsChampion = false, TeamId = 2 };
            Driver MV = new Driver() { Id = 3, Name = "Max Verstappen", Number = 33, DebutYear = "2014", IsChampion = false, TeamId = 3 };
            Driver SP = new Driver() { Id = 4, Name = "Sergio Perez", Number = 11, DebutYear = "2011", IsChampion = false, TeamId = 3 };
            
            Championship first = new Championship() { Id = 1, Year = 2008, WCC = Ferr.Id, NumberOfRaces = 18 };
            Championship second = new Championship() { Id = 2, Year = 2001, WCC = Ferr.Id, NumberOfRaces = 17 };
            Championship fourth = new Championship() { Id = 4, Year = 2011, WCC = RedB.Id, NumberOfRaces = 19 };
            Championship fifth = new Championship() { Id = 5, Year = 2012, WCC = RedB.Id, NumberOfRaces = 20 };
            Championship sixt = new Championship() { Id = 6, Year = 2013, WCC = RedB.Id, NumberOfRaces = 19 };
            Championship seventh = new Championship() { Id = 7, Year = 2014, WCC = Merc.Id, NumberOfRaces = 19 };

            mockDriverRepository.Setup((t) => t.ReadAll()).Returns(new List<Driver>() { LH, SV, FA, CL, CS, SP, MV }.AsQueryable());
            mockTeamRepository.Setup((t) => t.ReadAll()).Returns(new List<Team>() { Ferr,Alp,Ast,Alfa,Merc,RedB }.AsQueryable());
            mockChampionshipRepository.Setup((t) => t.ReadAll()).Returns(new List<Championship>() {first, second, fourth, fifth, sixt, seventh}.AsQueryable());

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

            Assert.That(result, Is.EqualTo(3));
        }
        [TestCase("Charles Leclerc")]
        [TestCase("Fernando Alonso")]
        [TestCase("Lewis Hamilton")]
        public void EvenNumbers(string name)
        {
            var result = dl.EvenNumbers();
            Assert.That(result.ToList(), Has.Exactly(1).Matches<Driver>(x=>x.Name==name));
        }
        [TestCase("Ferrari",21)]
        [TestCase("Renault",2)]
        [TestCase("Mercedes",7)]
        [TestCase("Honda",4)]
        public void SumChampsByEngine(string team, int won)
        {
            var result = tl.SumChampByEngines();
            Assert.AreEqual(4,result.Count());
            Assert.IsTrue(result.Any(x=>x.Key==team && x.Value==won));
        }
        [TestCase(2,2)]
        [TestCase(1,1)]
        public void Wins(int id, int number)
        {
            var result = cl.Wins(id);
            Assert.That(result, Is.EqualTo(number));
        }
        [TestCase(19)]
        public void RaceNumbers(int number)
        {
            var result = cl.RaceNumbers(number);
            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(result.Any(x => x.Year == 2011));
            Assert.IsTrue(result.Any(x => x.Year == 2013));
            Assert.IsTrue(result.Any(x => x.Year == 2014));
        }
        [Test]
        public void TeamReadAll()
        {
            Assert.That(() => { tl.ReadAll(); }, Is.Not.Null);
        }
        [TestCase("2018",false)]
        [TestCase("2007",true)]
        public void DebutedAndWon(string debutYear, bool isChampion)
        {
            var result = cl.DebutedAndIsChampion(debutYear);
            Assert.That(result, Is.EqualTo(isChampion));
        }
    }
}
