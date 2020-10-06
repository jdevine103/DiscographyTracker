using DiscographyTracker.Data;
using DiscographyTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscographyTracker.Services
{
    public class UserAlbumService
    {
        private readonly Guid _userId;

        public UserAlbumService(Guid userId)
        {
            _userId = userId;
        }
        public IEnumerable<UserAlbumListItem> GetUserAlbums(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var query =
                    db.UserAlbums
                    .Where(e => e.UserID == _userId.ToString() && e.Album.ArtistID == id )
                    .Select(
                        e =>
                        new UserAlbumListItem
                        {
                            UserAlbumID = e.UserAlbumID,
                            AlbumTitle = e.Album.AlbumTitle,
                            IsFavorited = e.IsFavorited,
                            HaveListened = e.HaveListened
                        });
                return query.ToArray();
            }
        }
        public UserAlbumDetail GetUserAlbumById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UserAlbums
                        .Single(e => e.UserAlbumID == id);
                return
                    new UserAlbumDetail
                    {
                        UserAlbumID = entity.UserAlbumID,
                        AlbumID = entity.AlbumID,
                    };
            }
        }
        public bool CreateUserAlbums(int id)
        {
            var svc = CreateArtistService();
            var model = svc.GetArtistById(id);

            int albumCount = 0;

            foreach (var album in model.Albums)
            {
                var entity =
                new UserAlbum()
                {
                    AlbumID = album.AlbumID,
                    UserID = _userId.ToString()
                };
                using (var db = new ApplicationDbContext())
                {
                    db.UserAlbums.Add(entity);
                    albumCount += db.SaveChanges();
                }

            }
            return albumCount == model.Albums.Count();
        }
        public bool UpdateUserAlbum(UserAlbumEdit model)
        {
            using (var db = new ApplicationDbContext())
            {
                var entity =
                    db
                        .UserAlbums
                        .Single(e => e.UserAlbumID == model.UserAlbumID);

                entity.UserAlbumID = model.UserAlbumID;
                entity.IsFavorited = model.IsFavorited;
                entity.HaveListened = model.HaveListened;
                return db.SaveChanges() == 1;
            }
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
        private ArtistService CreateArtistService()
        {
            var service = new ArtistService(_userId);
            return service;
        }
    }
}
