using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballBetting.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        public string FullName { get; set; }

        public decimal Balance { get; set; }

        public List<Bet> Bets { get; set; } = new List<Bet>();
    }
}
