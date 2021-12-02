using J2RXEK_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;

namespace J2RXEK_HFT_2021221.Data
{
    public class ChampionshipDBContext : DbContext
    {
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Championship> Championship { get; set; }

        public ChampionshipDBContext()
        {
            this.Database.EnsureCreated();
        }
        public ChampionshipDBContext(DbContextOptions<ChampionshipDBContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LocalDB.mdf;Integrated Security=True";
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

            Driver LH = new Driver() { Id = 1, TeamId = Merc.Id, Name = "Lewis Hamilton", Number = 44, Age = 36, DebutYear = "2007", IsChampion = true };
            Driver VB = new Driver() { Id = 2, TeamId = Merc.Id, Name = "Valtteri Bottas", Number = 77, Age = 32, DebutYear = "2013", IsChampion = false };
            Driver MV = new Driver() { Id = 3, TeamId = RedB.Id, Name = "Max Verstappen", Number = 33, Age = 24, DebutYear = "2014", IsChampion = false };
            Driver SP = new Driver() { Id = 4, TeamId = RedB.Id, Name = "Sergio Perez", Number = 11, Age = 31, DebutYear = "2011", IsChampion = false };
            Driver SV = new Driver() { Id = 5, TeamId = Ast.Id, Name = "Sebastian Vettel", Number = 5, Age = 34, DebutYear = "2007", IsChampion = true };
            Driver LS = new Driver() { Id = 6, TeamId = Ast.Id, Name = "Lance Stroll", Number = 18, Age = 23, DebutYear = "2017", IsChampion = false };
            Driver CL = new Driver() { Id = 7, TeamId = Ferr.Id, Name = "Charles Leclerc", Number = 16, Age = 24, DebutYear = "2018", IsChampion = false };
            Driver CS = new Driver() { Id = 8, TeamId = Ferr.Id, Name = "Carlos Sainz", Number = 55, Age = 27, DebutYear = "2015", IsChampion = false };
            Driver LN = new Driver() { Id = 9, TeamId = McL.Id, Name = "Lando Norris", Number = 4, Age = 22, DebutYear = "2019", IsChampion = false };
            Driver DR = new Driver() { Id = 10, TeamId = McL.Id, Name = "Daniel Ricciardo", Number = 3, Age = 32, DebutYear = "2011", IsChampion = false };
            Driver YT = new Driver() { Id = 11, TeamId = Alpha.Id, Name = "Yuki Tsunoda", Number = 22, Age = 21, DebutYear = "2021", IsChampion = false };
            Driver PG = new Driver() { Id = 12, TeamId = Alpha.Id, Name = "Pierre Gasly", Number = 10, Age = 25, DebutYear = "2017", IsChampion = false };
            Driver KR = new Driver() { Id = 13, TeamId = Alfa.Id, Name = "Kimi Raikkönen", Number = 7, Age = 42, DebutYear = "2001", IsChampion = true };
            Driver AG = new Driver() { Id = 14, TeamId = Alfa.Id, Name = "Antonio Giovinazzi", Number = 99, Age = 28, DebutYear = "2017", IsChampion = false };
            Driver FA = new Driver() { Id = 15, TeamId = Alp.Id, Name = "Fernando Alonso", Number = 14, Age = 40, DebutYear = "2001", IsChampion = true };
            Driver EO = new Driver() { Id = 16, TeamId = Alp.Id, Name = "Esteban Ocon", Number = 31, Age = 25, DebutYear = "2016", IsChampion = false };
            Driver GR = new Driver() { Id = 17, TeamId = Will.Id, Name = "George Russel", Number = 63, Age = 23, DebutYear = "2019", IsChampion = false };
            Driver NL = new Driver() { Id = 18, TeamId = Will.Id, Name = "Nicholas Latifi", Number = 6, Age = 26, DebutYear = "2020", IsChampion = false };
            Driver SCH = new Driver() { Id = 19, TeamId = Haas.Id, Name = "Mick Schumacher", Number = 47, Age = 22, DebutYear = "2021", IsChampion = false };
            Driver NM = new Driver() { Id = 20, TeamId = Haas.Id, Name = "Nikita Mazepin", Number = 9, Age = 22, DebutYear = "2021", IsChampion = false };

            Championship first = new Championship() { Id = 1, Year = 2008, WCC = Ferr.Id, NumberOfRaces = 18 };
            Championship second = new Championship() { Id = 2, Year = 2001, WCC = Ferr.Id, NumberOfRaces = 17 };
            Championship third = new Championship() { Id = 3, Year = 2010, WCC = RedB.Id, NumberOfRaces = 19 };
            Championship fourth = new Championship() { Id = 4, Year = 2011, WCC = RedB.Id, NumberOfRaces = 19 };
            Championship fifth = new Championship() { Id = 5, Year = 2012, WCC = RedB.Id, NumberOfRaces = 20 };
            Championship sixth = new Championship() { Id = 6, Year = 2013, WCC = RedB.Id, NumberOfRaces = 19 };
            Championship seventh = new Championship() { Id = 7, Year = 2014, WCC = Merc.Id, NumberOfRaces = 19 };

            modelbuilder.Entity<Team>().HasData(Merc, Alpha, RedB, Ferr, Alp, Ast, Alfa, Will, Haas, McL);
            modelbuilder.Entity<Driver>().HasData(LH, VB, PG, YT, SP, MV, CL, CS, EO, FA, SV, LS, KR, AG, GR, NL, SCH, NM, LN, DR);
            modelbuilder.Entity<Championship>().HasData(first, second, third, fourth, fifth, sixth, seventh);

        }

    }
}
