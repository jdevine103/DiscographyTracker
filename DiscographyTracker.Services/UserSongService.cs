using DiscographyTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscographyTracker.Services
{
    public class UserSongService
    {
        private readonly Guid _userId;

        public UserSongService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateUserSongs(int id)
        {
            var svc = CreateUserArtistService();
            var model = svc.GetUserArtistById(id);

            int songCount = 0;

            foreach (var album in model.UserAlbums)
            {
                songCount += album.UserSongs.Count();

                foreach (var song in album.UserSongs)
                {
                    var entity =
                    new UserSong()
                    {
                        SongID = song.SongID,
                        UserID = _userId.ToString()
                    };
                    using (var db = new ApplicationDbContext())
                    {
                        db.UserSongs.Add(entity);
                        songCount += db.SaveChanges();
                    }
                }
            }
            return songCount == model.UserAlbums.Count();
        }
        public bool DeleteUserAlbum(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UserAlbums
                        .Single(e => e.UserAlbumID == id);

                ctx.UserAlbums.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
        private UserArtistService CreateUserArtistService()
        {
            var service = new UserArtistService(_userId);
            return service;
        }
    }
}
