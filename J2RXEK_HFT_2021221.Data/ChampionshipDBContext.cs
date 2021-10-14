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
                entity.HasOne(driver => driver.Team)
                .WithMany(team => team.Drivers)
                .HasForeignKey(driver => driver.TeamName)
                .OnDelete(DeleteBehavior.Restrict);
                
            });
            modelbuilder.Entity<Team>(entity =>
            {
                entity.HasOne(team => team.Championship)
                .WithMany(championship => championship.Teams)
                .HasForeignKey(team => team.Location)
                .OnDelete(DeleteBehavior.Restrict);
            });

            Driver LH = new Driver() { Name = "Lewis Hamilton", Number = 44, DebutYear = "2007", IsChampion = true };
            Driver MV = new Driver() { Name = "Max Verstappen", Number = 33, DebutYear = "2014", IsChampion = false };
            Driver SV = new Driver() { Name = "Sebastian Vettel", Number = 5, DebutYear = "2007", IsChampion = true };
            Driver CL = new Driver() { Name = "Charles Leclerc", Number = 16, DebutYear = "2018", IsChampion = false };
            Driver LN = new Driver() { Name = "Lando Norris", Number = 4, DebutYear = "2019", IsChampion = false };
            Driver CS = new Driver() { Name = "Carlos Sainz", Number = 55, DebutYear = "2015", IsChampion = false };
            Driver PG = new Driver() { Name = "Pierre Gasly", Number = 10, DebutYear = "2017", IsChampion = false };
            Driver KR = new Driver() { Name = "Kimi Raikkönen", Number = 7, DebutYear = "2001", IsChampion = true };
            Driver FA = new Driver() { Name = "Fernando Alonso", Number = 14, DebutYear = "2001", IsChampion = true };
            Driver DR = new Driver() { Name = "Daniel Ricciardo", Number = 3, DebutYear = "2011", IsChampion = false };
            Driver GR = new Driver() { Name = "George Russel", Number = 63, DebutYear = "2019", IsChampion = false };
            Driver SCH = new Driver() { Name = "Mick Schumacher", Number = 47, DebutYear = "2021", IsChampion = false };
            Driver SP = new Driver() { Name = "Sergio Perez", Number = 11, DebutYear = "2011", IsChampion = false };

            Team Merc = new Team() { TeamName= "Mercedes",TeamPrincipals="Toto Wolff",PowerUnit="Mercedes",ChampionshipsWon=7 };
            Team Ferr = new Team() { TeamName = "Ferrari",TeamPrincipals="Mattia Binotto",PowerUnit="Ferrari",ChampionshipsWon=16 };
            Team RedB = new Team() { TeamName = "Red Bull",TeamPrincipals="Christian Horner",PowerUnit="Honda",ChampionshipsWon=4 };
            Team Alpha = new Team() { TeamName = "AlphaTauri",TeamPrincipals="Franz Tost",PowerUnit="Honda",ChampionshipsWon=0 };
            Team Alp = new Team() { TeamName = "Alpine",TeamPrincipals="Marcin Budkowski",PowerUnit="Renault",ChampionshipsWon=2 };
            Team Ast = new Team() { TeamName = "Aston Martin",TeamPrincipals="Otmar Szafnauer",PowerUnit="Mercedes",ChampionshipsWon=0 };
            Team Alfa = new Team() { TeamName = "Alfa Romeo",TeamPrincipals="Frederic Vasseur",PowerUnit="Ferrari",ChampionshipsWon=5 };
            Team Will = new Team() { TeamName = "Williams",TeamPrincipals="Jost Capito",PowerUnit="Mercedes",ChampionshipsWon=9 };
            Team Haas = new Team() { TeamName = "Haas",TeamPrincipals="Guenther Steiner",PowerUnit="Ferrari",ChampionshipsWon=0 };
            Team McL = new Team() { TeamName = "McLaren",TeamPrincipals="Andreas Seidl",PowerUnit="Mercedes",ChampionshipsWon=8 };

            Championship first = new Championship() {Location="Bahrain",Date=DateTime.Parse("2021.03.28")};
            Championship second = new Championship() { Location = "Italy", Date = DateTime.Parse("2021.04.18") };
            Championship third = new Championship() { Location = "Portugal", Date = DateTime.Parse("2021.05.02") };
            Championship fourth = new Championship() { Location ="Spain",Date=DateTime.Parse("2021.05.09")};
            Championship fifth = new Championship() { Location = "Monaco", Date = DateTime.Parse("2021.05.23") };
            Championship sixt = new Championship() { Location = "Azerbaijan", Date = DateTime.Parse("2021.06.06") };
            Championship sevent = new Championship() { Location = "France", Date = DateTime.Parse("2021.06.20") };

            modelbuilder.Entity<Driver>().HasData(LH,MV,SV,CL,LN,CS,PG,KR,FA,DR,GR,SCH,SP);
            modelbuilder.Entity<Team>().HasData(Merc,Ferr,RedB,Alpha,Ast,Alfa,Will,Haas,McL);
            modelbuilder.Entity<Championship>().HasData(first, second, third, fourth, fifth, sixt, sevent);

        }

    }
}
