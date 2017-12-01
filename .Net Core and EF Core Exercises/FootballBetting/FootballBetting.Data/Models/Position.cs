﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballBetting.Data.Models
{
    public class Position
    {
        [Key]
        [StringLength(2)]
        public string Id { get; set; }

        [Required]
        public string Description { get; set; }

        public List<Player> Players { get; set; } = new List<Player>();
    }
}
