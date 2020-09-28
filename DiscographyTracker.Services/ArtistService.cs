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
        public Artist CreateArtist(ArtistCreate model)
        {
            var entity =
                new Artist()
                {
                    ArtistName = model.ArtistName
                };

            using (var db = new ApplicationDbContext())
            {
                db.Artists.Add(entity);
                if (db.SaveChanges() == 1)
                {
                    return entity;
                }
                else
                    return null; 
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
        public ArtistDetail GetArtistById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Artists
                        .Single(e => e.ArtistID == id);
                return
                    new ArtistDetail
                    {
                        ArtistID = entity.ArtistID,
                        ArtistName = entity.ArtistName
                    };
            }
        }
        public bool UpdateArtist(ArtistEdit model)
        {
            using (var db = new ApplicationDbContext())
            {
                var entity =
                    db
                        .Artists
                        .Single(e => e.ArtistID == model.ArtistID);

                entity.ArtistName = model.ArtistName;

                return db.SaveChanges() == 1;
            }
        }
        public bool DeleteArtist(int artistID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Artists
                        .Single(e => e.ArtistID == artistID);

                ctx.Artists.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
