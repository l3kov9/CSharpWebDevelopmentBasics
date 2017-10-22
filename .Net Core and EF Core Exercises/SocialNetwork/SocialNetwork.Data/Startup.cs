using System;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using System.Linq;
using System.Collections.Generic;

namespace SocialNetwork
{
    public class Startup
    {
        private static Random random = new Random();

        public static void Main()
        {
            using (var db = new SocialNetworkDbContext())
            {
                db.Database.Migrate();

                // SeedUsers(db);

                // PrintAllUsersAndTheirFriendsCount(db);
                PrintAllUsersWithMoreThanFiveFriends(db);
            }
        }

        private static void PrintAllUsersWithMoreThanFiveFriends(SocialNetworkDbContext db)
        {
            var result = db
                .Users
                .Where(u => u.IsDeleted == false)
                .Where(u => (u.FromFriends.Count + u.ToFriends.Count) > 5)
                .OrderBy(u => u.RegisteredOn)
                .ThenBy(u => u.FromFriends.Count + u.ToFriends.Count)
                .Select(u => new
                {
                    Name = u.Username,
                    TotalFriends = u.FromFriends.Count + u.ToFriends.Count,
                    Period = DateTime.Now.Subtract(u.RegisteredOn.Value),
                })
                .ToList();

            foreach (var user in result)
            {
                Console.WriteLine($"{user.Name} - {user.TotalFriends} friends, period - {-user.Period.Days} days.");
            }
                
        }

        private static void PrintAllUsersAndTheirFriendsCount(SocialNetworkDbContext db)
        {
            var result = db
                .Users
                .Select(u => new
                {
                    u.Username,
                    TotalFriends = u.FromFriends.Count + u.ToFriends.Count,
                    Status = u.IsDeleted ? "Inactive" : "Active"
                })
                .OrderByDescending(u => u.TotalFriends)
                .ThenBy(u => u.Username);

            foreach (var user in result)
            {
                Console.WriteLine($"{user.Username} - {user.TotalFriends} friends, {user.Status}");
            }
        }

        private static void SeedUsers(SocialNetworkDbContext db)
        {
            const int totalUsers = 50;

            var biggestUserId = db.Users.Select(u => u.Id).FirstOrDefault();

            var allUsers = new List<User>();

            for (int i = biggestUserId; i < biggestUserId + totalUsers; i++)
            {
                var user = new User
                {
                    Username = $"Username {i}",
                    Password = $"Passw{i}r2@d#$",
                    Email = $"email@email{i}.com",
                    RegisteredOn = DateTime.Now.AddDays(100 + i * 10),
                    LastTimeLoggedIn = DateTime.Now.AddDays(i),
                    Age = i + 1
                };

                db.Add(user);
                allUsers.Add(user);
            }

            db.SaveChanges();

            var userIds = allUsers.Select(u => u.Id).ToList();

            for (int i = 0; i < userIds.Count; i++)
            {
                var currentUserId = userIds[i];

                var totalFriends = random.Next(5, 11);

                for (int j = 0; j < totalFriends; j++)
                {
                    var friendId = userIds[random.Next(0, userIds.Count)];

                    var validFriendShip = true;

                    // Cannot be friend to myself
                    if (friendId == currentUserId)
                    {
                        validFriendShip = false;
                    }

                    var friendShipExists = db
                        .FriendShips
                        .Any(f => (f.FromUserId == currentUserId && f.ToUserId == friendId)
                            || (f.FromUserId == friendId && f.ToUserId == currentUserId));

                    if (friendShipExists)
                    {
                        validFriendShip = false;
                    }

                    if (!validFriendShip)
                    {
                        j--;
                        continue;
                    }

                    db
                        .FriendShips
                        .Add(new FriendShip
                        {
                            FromUserId = currentUserId,
                            ToUserId = friendId
                        });

                    db.SaveChanges();
                }

            }
        }
    }
}
