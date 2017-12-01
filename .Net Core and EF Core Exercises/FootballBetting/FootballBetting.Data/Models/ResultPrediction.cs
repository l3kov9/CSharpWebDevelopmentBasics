using System.Collections.Generic;

namespace FootballBetting.Data.Models
{
    public class ResultPrediction
    {
        public int Id { get; set; }

        public string Prediction { get; set; }

        public List<BetGame> BetGames { get; set; } = new List<BetGame>();
    }
}
