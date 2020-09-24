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

        public SongService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateSong(SongCreate model)
        {
            var entity =
                new Song()
                {
                    AlbumID = model.AlbumID,
                    SongName = model.SongName,
                    HaveListened = model.HaveListened
                };
            using (var db = new ApplicationDbContext())
            {
                db.Songs.Add(entity);
                return db.SaveChanges() == 1;
            }
        }
    }
}
