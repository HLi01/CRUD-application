using Microsoft.EntityFrameworkCore;
using System;
using J2RXEK_HFT_2021221.Models;
using System.Collections.Generic;

namespace J2RXEK_HFT_2021221.Data
{
    public class ChampionshipDBContext : DbContext
    {
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Championship> Championship { get; set; }

        public ChampionshipDBContext()
        {
            this.Database.EnsureCreated();
        }
        //public ChampionshipDBContext(DbContextOptions<ChampionshipDBContext> options):base(options){ }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                string conn= @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LocalDB.mdf;Integrated Security=True";
                builder.UseLazyLoadingProxies().UseSqlServer(conn);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Driver>(entity =>
            {
                entity.HasOne(driver => driver.Team)
                .WithMany(team => team.Drivers)
                .HasForeignKey(driver => driver.TeamId)
                .OnDelete(DeleteBehavior.Restrict);
                
            });
            modelbuilder.Entity<Championship>(entity =>
            {
                entity.HasOne(championship => championship.Team)
                .WithMany(team => team.Championships)
                .HasForeignKey(championship => championship.WCC)
                .OnDelete(DeleteBehavior.Restrict);
            });

            Team Merc = new Team() { Id = 1, TeamName = "Mercedes", TeamPrincipal = "Toto Wolff", PowerUnit = "Mercedes", ChampionshipsWon = 7 };
            Team Ferr = new Team() { Id = 2, TeamName = "Ferrari", TeamPrincipal = "Mattia Binotto", PowerUnit = "Ferrari", ChampionshipsWon = 16 };
            Team RedB = new Team() { Id = 3, TeamName = "Red Bull", TeamPrincipal = "Christian Horner", PowerUnit = "Honda", ChampionshipsWon = 4 };
            Team Alpha = new Team() { Id = 4, TeamName = "AlphaTauri", TeamPrincipal = "Franz Tost", PowerUnit = "Honda", ChampionshipsWon = 0 };
            Team Alp = new Team() { Id = 5, TeamName = "Alpine", TeamPrincipal = "Marcin Budkowski", PowerUnit = "Renault", ChampionshipsWon = 2 };
            Team Ast = new Team() { Id = 6, TeamName = "Aston Martin", TeamPrincipal = "Otmar Szafnauer", PowerUnit = "Mercedes", ChampionshipsWon = 0 };
            Team Alfa = new Team() { Id = 7, TeamName = "Alfa Romeo", TeamPrincipal = "Frederic Vasseur", PowerUnit = "Ferrari", ChampionshipsWon = 5 };
            Team Will = new Team() { Id = 8, TeamName = "Williams", TeamPrincipal = "Jost Capito", PowerUnit = "Mercedes", ChampionshipsWon = 9 };
            Team Haas = new Team() { Id = 9, TeamName = "Haas", TeamPrincipal = "Guenther Steiner", PowerUnit = "Ferrari", ChampionshipsWon = 0 };
            Team McL = new Team() { Id = 10, TeamName = "McLaren", TeamPrincipal = "Andreas Seidl", PowerUnit = "Mercedes", ChampionshipsWon = 8 };

            Driver LH = new Driver() { Id=1, Name = "Lewis Hamilton", Number = 44, DebutYear = "2007", IsChampion = true, TeamId=Merc.Id };
            Driver VB = new Driver() { Id = 2, Name = "Valtteri Bottas", Number = 77, DebutYear = "2013", IsChampion = false, TeamId = Merc.Id };
            Driver MV = new Driver() { Id = 3, Name = "Max Verstappen", Number = 33, DebutYear = "2014", IsChampion = false, TeamId = RedB.Id };
            Driver SP = new Driver() { Id = 4, Name = "Sergio Perez", Number = 11, DebutYear = "2011", IsChampion = false, TeamId = RedB.Id };
            Driver SV = new Driver() { Id = 5, Name = "Sebastian Vettel", Number = 5, DebutYear = "2007", IsChampion = true, TeamId = Ast.Id };
            Driver LS = new Driver() { Id = 6, Name = "Lance Stroll", Number = 18, DebutYear = "2017", IsChampion = false, TeamId = Ast.Id };
            Driver CL = new Driver() { Id = 7, Name = "Charles Leclerc", Number = 16, DebutYear = "2018", IsChampion = false, TeamId = Ferr.Id };
            Driver CS = new Driver() { Id = 8, Name = "Carlos Sainz", Number = 55, DebutYear = "2015", IsChampion = false, TeamId = Ferr.Id };
            Driver LN = new Driver() { Id = 9, Name = "Lando Norris", Number = 4, DebutYear = "2019", IsChampion = false, TeamId = McL.Id };
            Driver DR = new Driver() { Id = 10, Name = "Daniel Ricciardo", Number = 3, DebutYear = "2011", IsChampion = false, TeamId = McL.Id };
            Driver YT = new Driver() { Id = 11, Name = "Yuki Tsunoda", Number = 22, DebutYear = "2021", IsChampion = false, TeamId = Alpha.Id };
            Driver PG = new Driver() { Id = 12, Name = "Pierre Gasly", Number = 10, DebutYear = "2017", IsChampion = false, TeamId = Alpha.Id };
            Driver KR = new Driver() { Id = 13, Name = "Kimi Raikkönen", Number = 7, DebutYear = "2001", IsChampion = true, TeamId = Alfa.Id };
            Driver AG = new Driver() { Id = 14, Name = "Antonio Giovinazzi", Number = 99, DebutYear = "2017", IsChampion = false, TeamId = Alfa.Id };
            Driver FA = new Driver() { Id = 15, Name = "Fernando Alonso", Number = 14, DebutYear = "2001", IsChampion = true, TeamId = Alp.Id };
            Driver EO = new Driver() { Id = 16, Name = "Esteban Ocon", Number = 31, DebutYear = "2016", IsChampion = false, TeamId = Alp.Id };
            Driver GR = new Driver() { Id = 17, Name = "George Russel", Number = 63, DebutYear = "2019", IsChampion = false, TeamId = Will.Id };
            Driver NL = new Driver() { Id = 18, Name = "Nicholas Latifi", Number = 6, DebutYear = "2020", IsChampion = false, TeamId = Will.Id };
            Driver SCH = new Driver() { Id = 19, Name = "Mick Schumacher", Number = 47, DebutYear = "2021", IsChampion = false, TeamId = Haas.Id };
            Driver NM = new Driver() { Id = 20, Name = "Nikita Mazepin", Number = 9, DebutYear = "2021", IsChampion = false, TeamId = Haas.Id };

            Championship first = new Championship() { Id=1, Year = 2008, WCC=Ferr.Id, NumberOfRaces=18 };
            Championship second = new Championship() { Id = 2, Year = 2001, WCC = Ferr.Id, NumberOfRaces =17};
            Championship third = new Championship() { Id = 3, Year = 2010, WCC = RedB.Id, NumberOfRaces = 19 };
            Championship fourth = new Championship() { Id = 4, Year = 2011, WCC = RedB.Id, NumberOfRaces = 19 };
            Championship fifth = new Championship() { Id = 5, Year = 2012, WCC = RedB.Id, NumberOfRaces = 20 };
            Championship sixt = new Championship() { Id = 6, Year = 2013, WCC = RedB.Id, NumberOfRaces = 19 };
            Championship seventh = new Championship() { Id = 7, Year = 2014, WCC = Merc.Id, NumberOfRaces = 19 };

            modelbuilder.Entity<Driver>().HasData(LH,VB,MV,SP,SV,LS,CL,CS,LN,DR,PG,YT,KR,AG,FA,EO,GR,NL,SCH,NM);
            modelbuilder.Entity<Team>().HasData(Merc,Ferr,RedB,Alpha,Ast,Alfa,Will,Haas,McL);
            modelbuilder.Entity<Championship>().HasData(first, second, third, fourth, fifth, sixt, seventh);

        }

    }
}
