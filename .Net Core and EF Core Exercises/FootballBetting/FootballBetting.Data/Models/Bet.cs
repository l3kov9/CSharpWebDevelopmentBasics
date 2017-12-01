using System;
using System.Collections.Generic;

namespace FootballBetting.Data.Models
{
    public class Bet
    {
        public int Id { get; set; }

        public decimal Money { get; set; }

        public DateTime BetTime { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public List<BetGame> BetGames { get; set; } = new List<BetGame>();
    }
}
