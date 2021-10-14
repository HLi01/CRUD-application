using Microsoft.EntityFrameworkCore;
using System;
using J2RXEK_HFT_2021221.Models;

namespace J2RXEK_HFT_2021221.Data
{
    public class ChampionshipDBContext : DbContext
    {
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Championship> Championships { get; set; }

        public ChampionshipDBContext()
        {
            this.Database.EnsureCreated();
        }
        public ChampionshipDBContext(DbContextOptions<ChampionshipDBContext> options):base(options){ }

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
                entity.HasOne(driver => driver.Team).WithMany(team => team.Drivers).HasForeignKey(driver => driver.Team).OnDelete(DeleteBehavior.Restrict);
                
            });
            modelbuilder.Entity<Team>(entity =>
            {
                entity.HasMany(team => team.Championship_Years).WithMany(championship => championship.Teams);

            });

            Driver LH = new Driver() { Name = "Lewis Hamilton", Number = 44, DebutYear = DateTime.Parse("2007"), IsChampion = true };
            Driver MV = new Driver() { Name = "Max Verstappen", Number = 33, DebutYear = DateTime.Parse("2014"), IsChampion = false };
            Driver SV = new Driver() { Name = "Sebastian Vettel", Number = 5, DebutYear = DateTime.Parse("2007"), IsChampion = true };
            Driver CL = new Driver() { Name = "Charles Leclerc", Number = 16, DebutYear = DateTime.Parse("2018"), IsChampion = false };
            Driver LN = new Driver() { Name = "Lando Norris", Number = 4, DebutYear = DateTime.Parse("2019"), IsChampion = false };
            Driver CS = new Driver() { Name = "Carlos Sainz", Number = 55, DebutYear = DateTime.Parse("2015"), IsChampion = false };
            Driver PG = new Driver() { Name = "Pierre Gasly", Number = 10, DebutYear = DateTime.Parse("2017"), IsChampion = false };
            Driver KR = new Driver() { Name = "Kimi Raikkönen", Number = 7, DebutYear = DateTime.Parse("2001"), IsChampion = true };
            Driver FA = new Driver() { Name = "Fernando Alonso", Number = 14, DebutYear = DateTime.Parse("2001"), IsChampion = true };
            Driver DR = new Driver() { Name = "Daniel Ricciardo", Number = 3, DebutYear = DateTime.Parse("2011"), IsChampion = false };
            Driver GR = new Driver() { Name = "George Russel", Number = 63, DebutYear = DateTime.Parse("2019"), IsChampion = false };
            Driver SCH = new Driver() { Name = "Mick Schumacher", Number = 47, DebutYear = DateTime.Parse("2021"), IsChampion = false };
            Driver SP = new Driver() { Name = "Sergio Perez", Number = 11, DebutYear = DateTime.Parse("2011"), IsChampion = false };

            Team Merc = new Team() { Name="Mercedes",TeamPrincipals="Toto Wolff",PowerUnit="Mercedes",ChampionshipsWon=7 };
            Team Ferr = new Team() { Name="Ferrari",TeamPrincipals="Mattia Binotto",PowerUnit="Ferrari",ChampionshipsWon=16 };
            Team RedB = new Team() { Name="Red Bull",TeamPrincipals="Christian Horner",PowerUnit="Honda",ChampionshipsWon=4 };
            Team Alpha = new Team() { Name="AlphaTauri",TeamPrincipals="Franz Tost",PowerUnit="Honda",ChampionshipsWon=0 };
            Team Alp = new Team() { Name="Alpine",TeamPrincipals="Marcin Budkowski",PowerUnit="Renault",ChampionshipsWon=2 };
            Team Ast = new Team() { Name="Aston Martin",TeamPrincipals="Otmar Szafnauer",PowerUnit="Mercedes",ChampionshipsWon=0 };
            Team Alfa = new Team() { Name="Alfa Romeo",TeamPrincipals="Frederic Vasseur",PowerUnit="Ferrari",ChampionshipsWon=5 };
            Team Will = new Team() { Name="Williams",TeamPrincipals="Jost Capito",PowerUnit="Mercedes",ChampionshipsWon=9 };
            Team Haas = new Team() { Name="Haas",TeamPrincipals="Guenther Steiner",PowerUnit="Ferrari",ChampionshipsWon=0 };
            Team McL = new Team() { Name="McLaren",TeamPrincipals="Andreas Seidl",PowerUnit="Mercedes",ChampionshipsWon=8 };

            Championship ten = new Championship() { Year=DateTime.Parse("2010")};
            Championship eleven = new Championship() { Year=DateTime.Parse("2011")};
            Championship twelve = new Championship() { Year=DateTime.Parse("2012")};
            Championship thirteen = new Championship() { Year=DateTime.Parse("2013")};
            Championship fourteen = new Championship() { Year=DateTime.Parse("2014")};
            Championship fifteen = new Championship() { Year=DateTime.Parse("2015")};
            Championship sixteen = new Championship() { Year=DateTime.Parse("2016")};
            Championship seventeen = new Championship() { Year=DateTime.Parse("2017")};
            Championship eighteen = new Championship() { Year=DateTime.Parse("2018")};
            Championship nineteen = new Championship() { Year=DateTime.Parse("2019")};
            Championship twenty = new Championship() { Year=DateTime.Parse("2020")};
            Championship twentyone = new Championship() { Year=DateTime.Parse("2021")};

        }

    }
}
