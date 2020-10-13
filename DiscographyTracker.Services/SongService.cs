using DiscographyTracker.Data;
using DiscographyTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscographyTracker.Services
{
    public class SongService
    {
        private readonly Guid _userId;
        public SongService() { }
        public SongService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateSong(SongCreate model)
        {
            var entity =
                new Song()
                {
                    AlbumID = model.AlbumID
                    //SongName = model.SongName,
                };
            using (var db = new ApplicationDbContext())
            {
                db.Songs.Add(entity);
                return db.SaveChanges() == 1;
            }
        }
        public IEnumerable<SongListItem> GetSongs()
        {
            using (var db = new ApplicationDbContext())
            {
                var query =
                    db.Songs
                    .Where(e => e.SongID > 0) //probably not proper
                    .Select(
                        e =>
                        new SongListItem
                        {
                            ArtistName = e.Album.Artist.ArtistName,
                            AlbumTitle = e.Album.AlbumTitle,
                            ArtistID = e.Album.ArtistID,
                            SongName = e.SongName,
                            SongID = e.SongID,
                            AlbumID = e.AlbumID
                        });
                return query.ToArray();
            }
        }
        public SongDetail GetSongById(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var entity =
                    db
                        .Songs
                        .Single(e => e.SongID == id);
                return
                    new SongDetail
                    {
                        ArtistName = entity.Album.Artist.ArtistName,
                        SongID = entity.SongID,
                        AlbumTitle = entity.Album.AlbumTitle,
                        AlbumID = entity.AlbumID,
                        SongName = entity.SongName,
                    };
            }
        }
        public bool UpdateSong(SongEdit model)
        {
            using (var db = new ApplicationDbContext())
            {
                var entity =
                    db
                        .Songs
                        .Single(e => e.SongID == model.SongID);

                entity.SongID = model.SongID;
                entity.AlbumID = model.AlbumID;
                entity.SongName = model.SongName;

                return db.SaveChanges() == 1;
            }
        }
        public bool DeleteSong(int songID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Songs
                        .Single(e => e.SongID == songID);

                ctx.Songs.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
