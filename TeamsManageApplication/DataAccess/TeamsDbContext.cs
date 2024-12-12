using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Emit;
using Teams.Entities;

namespace TeamsManageApplication.DataAccess
{
    public class TeamsDbContext : DbContext
    {
        public TeamsDbContext(DbContextOptions<TeamsDbContext> options)
        : base(options) { }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map enum values --> strings in DB based on enum name:
            modelBuilder.Entity<Game>()
                .Property(g => g.GameType)
                .HasConversion<string>()
                .HasMaxLength(64);

            // Seed some Teams:
            modelBuilder.Entity<Team>().HasData(
                new Team() { TeamId = 1, Name = "Toronto Maple Leafs", DateCreated = new DateTime(2024, 1, 7) },
                new Team() { TeamId = 2, Name = "Winnipeg Jets", DateCreated = new DateTime(2024, 1, 7) }
            );

            // Seed some Players:
            modelBuilder.Entity<Player>().HasData(
                new Player() { PlayerId = 1, LeagueRegistrationNumber = "DF-17345677", FirstName = "Bart", LastName = "Simpson", Number = 9, TeamId = 1 },
                new Player() { PlayerId = 2, LeagueRegistrationNumber = "AB-99685633", FirstName = "Lisa", LastName = "Simpson", Number = 11, TeamId = 1 },
                new Player() { PlayerId = 3, LeagueRegistrationNumber = "FG-87455945", FirstName = "Maggie", LastName = "Simpson", Number = 18, TeamId = 2 },
                new Player() { PlayerId = 4, LeagueRegistrationNumber = "LM-11409213", FirstName = "Marge", LastName = "Simpson", Number = 27, TeamId = 2 },
                new Player() { PlayerId = 5, LeagueRegistrationNumber = "ZC-59308990", FirstName = "Homer", LastName = "Simpson", Number = 31, TeamId = 2 }
            );

            // Seed some Games:
            modelBuilder.Entity<Game>().HasData(
                new Game() { GameId = 1, Date = new DateTime(2024, 12, 7), OpposingTeamName = "Edmonton Oilers", GameType = GameTypeOptions.Home, TeamId = 1 },
                new Game() { GameId = 2, Date = new DateTime(2024, 12, 5), OpposingTeamName = "Calgary Flames", GameType = GameTypeOptions.Home, TeamId = 1 },
                new Game() { GameId = 3, Date = new DateTime(2023, 12, 28), OpposingTeamName = "Montreal Canadians", GameType = GameTypeOptions.Away, TeamId = 2 },
                new Game() { GameId = 4, Date = new DateTime(2023, 12, 22), OpposingTeamName = "Ottawa Senators", GameType = GameTypeOptions.Away, TeamId = 2 },
                new Game() { GameId = 5, Date = new DateTime(2023, 12, 20), OpposingTeamName = "New York Rangers", GameType = GameTypeOptions.Home, TeamId = 2 },
                new Game() { GameId = 6, Date = new DateTime(2023, 12, 11), OpposingTeamName = "Chicago Blackhawks", GameType = GameTypeOptions.Home, TeamId = 1 },
                new Game() { GameId = 7, Date = new DateTime(2023, 12, 8), OpposingTeamName = "Detroit Redwings", GameType = GameTypeOptions.Away, TeamId = 1 },
                new Game() { GameId = 8, Date = new DateTime(2023, 11, 5), OpposingTeamName = "Boston Bruins", GameType = GameTypeOptions.Away, TeamId = 2 },
                new Game() { GameId = 9, Date = new DateTime(2023, 11, 1), OpposingTeamName = "Philadelphia Flyers", GameType = GameTypeOptions.Home, TeamId = 2 }
            );
        }

    }
}
