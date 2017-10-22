using SocialNetwork.Data.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Data
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        [Password]
        [MinLength(6)]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        // TODO: Validate email address
        public string Email { get; set; }

        [MaxLength(1024)]
        public byte[] ProfilePicture { get; set; }

        public DateTime? RegisteredOn { get; set; }

        public DateTime? LastTimeLoggedIn { get; set; }

        [Range(1,120)]
        public int? Age { get; set; }

        public bool IsDeleted { get; set; }

        public List<FriendShip> FromFriends { get; set; } = new List<FriendShip>();

        public List<FriendShip> ToFriends { get; set; } = new List<FriendShip>();

        public List<Album> Albums { get; set; } = new List<Album>();
    }
}
