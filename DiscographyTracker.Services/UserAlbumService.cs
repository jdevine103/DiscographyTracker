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
        public IEnumerable<UserAlbumListItem> GetUserAlbums()
        {
            using (var db = new ApplicationDbContext())
            {
                var query =
                    db.UserAlbums
                    .Where(e => e.AlbumID > 0) //probably not proper
                    .Select(
                        e =>
                        new UserAlbumListItem
                        {
                            AlbumID = e.AlbumID,
                            UserID = e.UserID
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
