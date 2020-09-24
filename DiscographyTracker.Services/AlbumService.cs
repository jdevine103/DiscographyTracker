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

        public AlbumService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateAlbum(AlbumCreate model)
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
                return db.SaveChanges() == 1;
            }
        }
    }
}
