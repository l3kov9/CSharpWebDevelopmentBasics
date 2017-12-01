using System;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using System.Linq;
using System.Collections.Generic;
using SocialNetwork.Data.Logic;

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
                // SeedAlbumsAndPictures(db);
                // SeedTags(db);

                // PrintAllUsersAndTheirFriendsCount(db);
                // PrintAllUsersWithMoreThanFiveFriends(db);
                // PrintAlbumsWithTotalPictures(db);
                // PrintPicturesInfo(db);
                // PrintAlbumsByUser(db);
                // PrintAlbumByTag(db);
                // PrintUsersWithMoreThanThreeAlbums(db);
            }
        }

        private static void PrintUsersWithMoreThanThreeAlbums(SocialNetworkDbContext db)
        {
            var result = db
                .Users
                .Where(u => u.Albums.Any(a => a.Tags.Count > 3))
                .Select(u => new
                {
                    u.Username,
                    Albums = u.Albums
                        .Where(a => a.Tags.Count > 3)
                        .Select(a => new
                        {
                            a.Name,
                            Tags = a.Tags.Select(t => t.Tag.Name)
                        })
                        .ToList()
                })
                .OrderByDescending(u => u.Albums.Count())
                .ThenByDescending(u => u.Albums.Sum(t=>t.Tags.Count()))
                .ThenBy(u=>u.Username)
                .ToList();

            foreach (var user in result)
            {
                Console.WriteLine($"Name: {user.Username}");

                foreach (var album in user.Albums)
                {
                    Console.WriteLine($"-----{album.Name} - {string.Join(", ",album.Tags)}");
                }

                Console.WriteLine(new string('*',50));
            }
        }

        private static void PrintAlbumByTag(SocialNetworkDbContext db)
        {
            var tag = "#tag0";

            var result = db
                .Albums
                .Where(a => a.Tags.Any(t => t.Tag.Name == tag))
                .OrderByDescending(a => a.Tags.Count())
                .ThenBy(a => a.Name)
                .Select(a => new
                {
                    Title = a.Name,
                    Owner = a.User.Username
                })
                .ToList();

            foreach (var album in result)
            {
                Console.WriteLine($"{album.Title} - {album.Owner}");
            }
        }

        private static void SeedTags(SocialNetworkDbContext db)
        {
            int totalTags = db.Albums.Count() * 20;

            var tags = new List<Tag>();

            for (int i = 0; i < totalTags; i++)
            {
                var tag = new Tag
                {
                    Name = TagTransformer.Transform($"tag{i}")
                };

                tags.Add(tag);
                db
                    .Tags
                    .Add(tag);
            }

            db.SaveChanges();

            var albumIds = db
                .Albums
                .Select(a => a.Id)
                .ToList();

            foreach (var tag in tags)
            {
                var totalAlbums = random.Next(0, 20);

                for (int i = 0; i < totalAlbums; i++)
                {
                    var albumId = albumIds[random.Next(0, albumIds.Count)];

                    var tagExistsForAlbum = db
                        .Albums
                        .Any(a => a.Id == albumId && a.Tags.Any(t => t.TagId==tag.Id));

                    if (tagExistsForAlbum)
                    {
                        i--;
                        continue;
                    }

                    tag
                        .Albums
                        .Add(new AlbumTag
                        {
                            AlbumId = albumId
                        });

                    db.SaveChanges();
                }
            }
        }

        private static void PrintAlbumsByUser(SocialNetworkDbContext db)
        {
            var userId = 2;

            var result = db
                .Albums
                .Where(a => a.UserId == userId)
                .Select(a => new
                {
                    Name = a.User.Username,
                    a.IsPublic,
                    Pictures = a.Pictures.Select(p => new
                    {
                        p.Picture.Title,
                        p.Picture.Path
                    })
                })
                .OrderBy(a=>a.Name)
                .ToList();

            foreach (var album in result)
            {
                Console.WriteLine($"Name: {album.Name}");

                if (album.IsPublic == true)
                {
                    foreach (var picture in album.Pictures)
                    {
                        Console.WriteLine($"{picture.Title} - {picture.Path}");
                    }

                }
                else
                {
                    Console.WriteLine("Private content!");
                }

                Console.WriteLine(new string('-', 50));

            }
        }

        private static void PrintPicturesInfo(SocialNetworkDbContext db)
        {
            var result = db
                .Pictures
                .Where(p => p.Albums.Count > 2)
                .OrderByDescending(p => p.Albums.Count)
                .ThenBy(p => p.Title)
                .Select(p => new
                {
                    Title = p.Title,
                    Albums = p
                        .Albums
                        .Select(a => new
                        {
                            a.Album.Name,
                            a.Album.User.Username
                        })

                })
                .ToList();

            var counter = 1;

            foreach (var picture in result)
            {
                Console.WriteLine($"Picture {counter} - Title: {picture.Title}");

                foreach (var albumInfo in picture.Albums)
                {
                    Console.WriteLine($"Album name: {albumInfo.Name}, owner: {albumInfo.Username}");
                }

                Console.WriteLine(new string('-',50));
                counter++;
            }
        }

        private static void PrintAlbumsWithTotalPictures(SocialNetworkDbContext db)
        {
            db
                .Albums
                .Select(a => new
                {
                    Title = a.Name,
                    Owner = a.User.Username,
                    TotalPictures = a.Pictures.Count
                })
                .OrderByDescending(a => a.TotalPictures)
                .ThenBy(a => a.Owner)
                .ToList()
                .ForEach(a => Console.WriteLine($"{a.Title} - {a.Owner}, {a.TotalPictures} pictures."));
        }

        private static void SeedAlbumsAndPictures(SocialNetworkDbContext db)
        {
            const int totalAlbums = 100;
            const int totalPictures = 500;

            var biggestAlbumId = db
                .Albums
                .OrderByDescending(a => a.Id)
                .Select(a => a.Id)
                .FirstOrDefault() + 1;

            var usersIds = db
                .Users
                .Select(u => u.Id)
                .ToList();

            var albums = new List<Album>();

            for (int i = biggestAlbumId; i < biggestAlbumId + totalAlbums; i++)
            {
                var album = new Album
                {
                    Name = $"Album {i}",
                    BackgroundColor = $"Color {i}",
                    IsPublic = random.Next(0, 2) == 0 ? true : false,
                    UserId = usersIds[random.Next(0, usersIds.Count)]
                };

                db
                    .Albums
                    .Add(album);

                albums
                    .Add(album);
            }

            db.SaveChanges();

            var biggestPictureId = db
                .Pictures
                .OrderByDescending(p => p.Id)
                .Select(p => p.Id)
                .FirstOrDefault() + 1;

            var pictures = new List<Picture>();

            for (int i = biggestPictureId; i < biggestPictureId + totalPictures; i++)
            {
                var picture = new Picture
                {
                    Title = $"Picture {i}",
                    Caption = $"Caption {i}",
                    Path = $"Path {i}"
                };

                db
                    .Pictures
                    .Add(picture);
                pictures.Add(picture);
            }

            db.SaveChanges();

            var albumIds = albums
                .Select(a => a.Id)
                .ToList();

            var pictureIds = pictures
                .Select(p => p.Id)
                .ToList();

            for (int i = 0; i < pictures.Count; i++)
            {
                var picture = pictures[i];

                var numberOfAlbums = random.Next(0, 20);

                for (int j = 0; j < numberOfAlbums; j++)
                {
                    var albumId = albumIds[random.Next(0, albumIds.Count)];

                    var pictureExistsInAlbums = db
                        .Pictures
                        .Any(p => p.Id == picture.Id && p.Albums.Any(a => a.AlbumId == albumId));

                    if (pictureExistsInAlbums)
                    {
                        j--;
                        continue;
                    }

                    picture.Albums.Add(new AlbumPicture
                    {
                        AlbumId = albumId,
                    });

                    db.SaveChanges();
                }
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
