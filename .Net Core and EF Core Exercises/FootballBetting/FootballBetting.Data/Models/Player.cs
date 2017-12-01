using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballBetting.Data.Models
{
    public class Player
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int SquadNumber { get; set; }

        public int TeamId { get; set; }

        public Team Team { get; set; }

        public string PositionId { get; set; }

        public Position Position { get; set; }

        public bool IsInjured { get; set; }

        public List<PlayerStatistic> PlayerStatistics { get; set; } = new List<PlayerStatistic>();
    }
}
