using DiscographyTracker.Data;
using DiscographyTracker.Models;
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
        public IEnumerable<UserSongListItem> GetUserSongs(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var query =
                    db.UserSongs
                    .Where(e => e.UserID == _userId.ToString() && e.Song.AlbumID == id)
                    .Select(
                        e =>
                        new UserSongListItem
                        {
                            UserSongID = e.UserSongID,
                            SongName = e.Song.SongName,
                            IsFavorited = e.IsFavorited,
                            HaveListened = e.HaveListened
                        });
                return query.ToArray();
            }
        }
        public UserSongDetail GetUserSongById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UserSongs
                        .Single(e => e.UserSongID == id);
                return
                    new UserSongDetail
                    {
                        UserSongID = entity.UserSongID,
                        SongName = entity.Song.SongName,
                        IsFavorited = entity.IsFavorited,
                        HaveListened = entity.HaveListened
                    };
            }
        }
        public bool UpdateUserSong(UserSongEdit model)
        {
            using (var db = new ApplicationDbContext())
            {
                var entity =
                    db
                        .UserSongs
                        .Single(e => e.UserSongID == model.UserSongID);

                entity.IsFavorited = model.IsFavorited;
                entity.HaveListened = model.HaveListened;
                return db.SaveChanges() == 1;
            }
        }
        public bool DeleteUserSong(int id)
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
    }
}
