using FootballBetting.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballBetting.Data.FottballBettingDb
{
    public class FootballBettingDbContext : DbContext
    {
        public DbSet<Bet> Bets { get; set; }

        public DbSet<BetGame> BetGames { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Competition> Competitions { get; set; }

        public DbSet<CompetitionType> CompetitionTypes { get; set; }

        public DbSet<Continent> Continents { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<ResultPrediction> ResultPredictions { get; set; }

        public DbSet<Round> Rounds { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<User> Users { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder
                .UseSqlServer("Server=.;Database=FootballBettingDb;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<BetGame>()
                .HasKey(bg => new
                {
                    bg.BetId,
                    bg.GameId
                });

            builder
                .Entity<PlayerStatistic>()
                .HasKey(ps => new
                {
                    ps.GameId,
                    ps.PlayerId
                });

            builder
                .Entity<Team>()
                .HasOne(t => t.PrimaryKitColor)
                .WithMany(c => c.PrimaryKits)
                .HasForeignKey(c => c.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Team>()
                .HasOne(t => t.SecondaryKitColor)
                .WithMany(c => c.SecondaryKits)
                .HasForeignKey(c => c.SecondaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Team>()
                .HasOne(t => t.Town)
                .WithMany(t => t.Teams)
                .HasForeignKey(t => t.TownId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Team>()
                .HasMany(t => t.Players)
                .WithOne(p => p.Team)
                .HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Team>()
                .HasMany(t => t.HomeTeams)
                .WithOne(ht => ht.HomeTeam)
                .HasForeignKey(t => t.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Team>()
                .HasMany(t => t.AwayTeams)
                .WithOne(at => at.AwayTeam)
                .HasForeignKey(at => at.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Country>()
                .HasMany(c => c.Towns)
                .WithOne(t => t.Country)
                .HasForeignKey(t => t.CountryId);

            builder
                .Entity<Country>()
                .HasOne(c => c.Continent)
                .WithMany(c => c.Countries)
                .HasForeignKey(c => c.ContinentId);

            builder
                .Entity<Player>()
                .HasOne(p => p.Position)
                .WithMany(p => p.Players)
                .HasForeignKey(p => p.PositionId);

            builder
                .Entity<Player>()
                .HasMany(p => p.PlayerStatistics)
                .WithOne(ps => ps.Player)
                .HasForeignKey(ps => ps.PlayerId);

            builder
                .Entity<Game>()
                .HasMany(g => g.PlayerStatistics)
                .WithOne(ps => ps.Game)
                .HasForeignKey(ps => ps.GameId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Game>()
                .HasOne(g => g.Round)
                .WithMany(r => r.Games)
                .HasForeignKey(g => g.RoundId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Game>()
                .HasMany(g => g.BetGames)
                .WithOne(bg => bg.Game)
                .HasForeignKey(bg => bg.GameId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Competition>()
                .HasOne(c => c.Type)
                .WithMany(ct => ct.Competitions)
                .HasForeignKey(c => c.CompetitionTypeId);

            builder
                .Entity<Competition>()
                .HasMany(c => c.Games)
                .WithOne(g => g.Competition)
                .HasForeignKey(g => g.CompetitionId);

            builder
                .Entity<Bet>()
                .HasMany(b => b.BetGames)
                .WithOne(bg => bg.Bet)
                .HasForeignKey(bg => bg.BetId);

            builder
                .Entity<BetGame>()
                .HasOne(bg => bg.ResultPrediction)
                .WithMany(rp => rp.BetGames)
                .HasForeignKey(bg => bg.ResultPredictionId);

            builder
                .Entity<User>()
                .HasMany(u => u.Bets)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);
        }
    }
}
