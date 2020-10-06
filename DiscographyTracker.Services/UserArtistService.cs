using DiscographyTracker.Data;
using DiscographyTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscographyTracker.Services
{
    public class UserArtistService
    {
        private readonly Guid _userId;

        public UserArtistService(Guid userId)
        {
            _userId = userId;
        }

        public IEnumerable<UserArtistListItem> GetUserArtists()
        {
            using (var db = new ApplicationDbContext())
            {
                var query =
                    db.UserArtists
                    .Where(e => e.ArtistID > 0) //probably not proper
                    .Select(
                        e =>
                        new UserArtistListItem
                        {
                            ArtistName = e.Artist.ArtistName,
                            UserArtistID = e.UserArtistID,
                            ArtistID = e.ArtistID,
                            UserID = e.UserID,
                            UserAlbums = e.UserAlbums.Select(k => new UserAlbumDetail
                            {
                                AlbumID = k.AlbumID,
                                IsFavorited = k.IsFavorited,
                                AlbumTitle = k.Album.AlbumTitle
                            }).ToList()
                        }); ;
                return query.ToList();
            }
        }
        public UserArtistDetail GetUserArtistById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UserArtists
                        .Single(e => e.UserArtistID == id);
                return
                    new UserArtistDetail
                    {
                        ArtistID = entity.ArtistID,
                    };
            }
        }
        public bool CreateUserArtist(UserArtistCreate model)
        {
            var entity =
            new UserArtist()
            {
                ArtistID = model.ArtistID,
                UserID = _userId.ToString()
            };

            using (var db = new ApplicationDbContext())
            {
                if (db.UserArtists.Any(e => e.ArtistID.Equals(entity.ArtistID)))
                {
                    return false;
                }
                else
                {
                    db.UserArtists.Add(entity);
                    
                }
                return db.SaveChanges() == 1;
            }
        }
        public bool DeleteUserArtist(int artistID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UserArtists
                        .Single(e => e.UserArtistID == artistID);

                ctx.UserArtists.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
        private UserAlbumService CreateUserAlbumService()
        {
            var service = new UserAlbumService(_userId);
            return service;
        }
    }
}
