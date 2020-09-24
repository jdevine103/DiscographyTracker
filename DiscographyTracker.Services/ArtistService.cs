using DiscographyTracker.Data;
using DiscographyTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscographyTracker.Services
{
    public class ArtistService
    {
        private readonly Guid _userId;

        public ArtistService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateArtist(ArtistCreate model)
        {
            var entity =
                new Artist()
                {
                    ArtistName = model.ArtistName
                };

            using (var db = new ApplicationDbContext())
            {
                db.Artists.Add(entity);
                return db.SaveChanges() ==1;
            }
        }
        public IEnumerable<ArtistListItem> GetArtists()
        {
            using (var db = new ApplicationDbContext())
            {
                var query =
                    db.Artists
                    .Where(e => e.ArtistID > 0) //probably not proper
                    .Select(
                        e =>
                        new ArtistListItem
                        {
                            ArtistID = e.ArtistID,
                            ArtistName = e.ArtistName
                        });
                return query.ToArray();
            }
        }
    }
}
