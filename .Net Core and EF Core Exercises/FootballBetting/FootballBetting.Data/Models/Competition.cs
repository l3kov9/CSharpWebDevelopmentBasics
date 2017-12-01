using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballBetting.Data.Models
{
    public class Competition
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int CompetitionTypeId { get; set; }

        public CompetitionType Type { get; set; }

        public List<Game> Games { get; set; } = new List<Game>();
    }
}
