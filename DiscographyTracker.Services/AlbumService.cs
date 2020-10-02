using DiscographyTracker.Data;
using DiscographyTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscographyTracker.Services
{
    public class AlbumService
    {
        private readonly Guid _userId;
        public AlbumService() { }
        public AlbumService(Guid userId)
        {
            _userId = userId;
        }
        public Album CreateAlbum(AlbumCreate model)
        {
            var entity =
                new Album()
                {
                    ArtistID = model.ArtistID,
                    AlbumTitle = model.AlbumTitle,
                    ReleaseDate = model.ReleaseDate
                };
            using (var db = new ApplicationDbContext())
            {
                db.Albums.Add(entity);
                db.SaveChanges();
                return entity;
            }
        }
        public IEnumerable<AlbumListItem> GetAlbums()
        {
            using (var db = new ApplicationDbContext())
            {
                var query =
                    db.Albums
                    .Where(e => e.ArtistID > 0) //probably not proper
                    .Select(
                        e =>
                        new AlbumListItem
                        {
                            AlbumID = e.AlbumID,
                            ArtistID = e.ArtistID,
                            AlbumTitle = e.AlbumTitle,
                            ReleaseDate = e.ReleaseDate
                        });
                return query.ToArray();
            }
        }
        public AlbumDetail GetAlbumById(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var entity =
                    db
                        .Albums
                        .Single(e => e.AlbumID == id);
                return
                    new AlbumDetail
                    {
                        AlbumID = id,
                        ArtistID = entity.ArtistID,
                        ArtistName = entity.Artist.ArtistName,
                        AlbumTitle = entity.AlbumTitle,
                        ReleaseDate = entity.ReleaseDate,
                        Songs = entity.Songs.Select(e =>  new SongDetail
                        {
                            SongID = e.SongID,
                            SongName = e.SongName,
                        })
                    };
            }
        }
        public bool UpdateAlbum(AlbumEdit model)
        {
            using (var db = new ApplicationDbContext())
            {
                var entity =
                    db
                        .Albums
                        .Single(e => e.AlbumID == model.AlbumID);

                entity.ArtistID = model.ArtistID;
                entity.AlbumTitle = model.AlbumTitle;
                entity.ReleaseDate = model.ReleaseDate;

                return db.SaveChanges() == 1;
            }
        }
        public bool DeleteAlbum(int albumID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Albums
                        .Single(e => e.AlbumID == albumID);

                ctx.Albums.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
