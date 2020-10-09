using DiscographyTracker.Data;
using DiscographyTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscographyTracker.Services
{
    public class UserArtistService
    {
        private readonly Guid _userId;

        public UserArtistService(Guid userId)
        {
            _userId = userId;
        }
        public UserArtistDetail GetUserArtistById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UserArtists
                        .Single(e => e.UserArtistID == id);
                return
                    new UserArtistDetail
                    {
                        ArtistID = entity.ArtistID,
                    };
            }
        }
        public bool DeleteUserArtist(int artistID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .UserArtists
                        .Single(e => e.UserArtistID == artistID);

                ctx.UserArtists.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
