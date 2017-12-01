using System;
using System.Collections.Generic;

namespace FootballBetting.Data.Models
{
    public class Game
    {
        public int Id { get; set; }

        public int HomeTeamId { get; set; }

        public Team HomeTeam { get; set; }

        public int AwayTeamId { get; set; }

        public Team AwayTeam { get; set; }

        public List<PlayerStatistic> PlayerStatistics { get; set; } = new List<PlayerStatistic>();

        public int HomeGoals { get; set; }

        public int AwayGoals { get; set; }

        public DateTime Date { get; set; }

        public decimal HomeTeamWinBetRate { get; set; }

        public decimal AwayTeamWinBetRate { get; set; }

        public decimal DrawGameBetRate { get; set; }

        public int RoundId { get; set; }

        public Round Round { get; set; }

        public int CompetitionId { get; set; }

        public Competition Competition { get; set; }

        public List<BetGame> BetGames { get; set; } = new List<BetGame>();
    }
}
