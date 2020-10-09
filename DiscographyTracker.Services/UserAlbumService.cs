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
                        IsFavorited = entity.IsFavorited,
                        HaveListened = entity.HaveListened
                    };
            }
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
    }
}
