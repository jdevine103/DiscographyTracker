using DiscographyTracker.Data;
using DiscographyTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
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
        public IEnumerable<UserArtistListItem> GetCrate()
        {
            using (var ctx = new ApplicationDbContext())
            {//we get our user
                var entity =
                    ctx
                        .Users
                        .FirstOrDefault(e => e.Id == _userId.ToString());
                // we get our crate
                IEnumerable<UserArtistListItem> crate = entity.UserArtists.Select(
                    e => new UserArtistListItem
                    {
                        ArtistName = e.Artist.ArtistName,
                        ArtistID = e.ArtistID,
                        UserArtistID = e.UserArtistID,
                        UserID = _userId.ToString(),
                        //UserAlbums = e.Artist.Albums.SelectMany(a=> a.UserAlbums.Where(u=>u.UserID == _userId.ToString()))
                        UserAlbums = e.User.UserAlbums.Where(k => k.Album.ArtistID == e.ArtistID)
                            .Select(j => new UserAlbumDetail
                            {
                                AlbumTitle = j.Album.AlbumTitle,
                                IsFavorited = j.IsFavorited,
                                HaveListened = j.HaveListened
                            }).ToList()
                        //        UserSongs = e.User.UserSongs.Where(i => i.UserAlbumID == j.UserAlbumID)
                        //            .Select(q => new UserSongDetail
                        //            {
                        //                UserAlbumID = j.UserAlbumID,
                        //                IsFavorited = j.IsFavorited,
                        //                HaveListened = j.HaveListened
                        //            })
                        //    }).ToList()
                    });

                return crate.ToList();
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
                        //UserAlbums = entity.UserAlbums.Select(k => new UserAlbumDetail
                        //{
                        //    AlbumTitle = k.Album.AlbumTitle,
                        //    IsFavorited = k.IsFavorited,
                        //    HaveListened = k.HaveListened,
                        //}).ToList()
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
