using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballBetting.Data.Models
{
    public class CompetitionType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Competition> Competitions { get; set; } = new List<Competition>();
    }
}
