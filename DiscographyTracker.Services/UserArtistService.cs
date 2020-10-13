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
        public bool DeleteUserArtist(int artistID)
        {
            int userAlbumCount = 0;
            int userSongCount = 0;
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UserArtists
                        .FirstOrDefault(e => e.UserArtistID == artistID);
                var userAlbums =
                    ctx.UserAlbums.Where(i => i.UserArtistID == entity.UserArtistID).ToList();
                userAlbumCount = userAlbums.Count();

                foreach (var userAlbum in userAlbums)
                {
                    var userSongs = ctx.UserSongs.Where(e => e.UserAlbumID == userAlbum.UserAlbumID).ToList();
                    //userSongCount = userSongs.Count();  -- needs MARS
                    foreach (var userSong in userSongs)
                    {
                        ctx.UserSongs.Remove(userSong);
                        userSongCount++;
                    }

                    ctx.UserAlbums.Remove(userAlbum);
                }
                
                ctx.UserArtists.Remove(entity);

                return ctx.SaveChanges() == 1 + userAlbumCount + userSongCount;
            }
        }
    }
}
