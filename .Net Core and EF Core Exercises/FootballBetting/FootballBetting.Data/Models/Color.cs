using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballBetting.Data.Models
{
    public class Color
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Team> PrimaryKits { get; set; } = new List<Team>();

        public List<Team> SecondaryKits { get; set; } = new List<Team>();
    }
}
