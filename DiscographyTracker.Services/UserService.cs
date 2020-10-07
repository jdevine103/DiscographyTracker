using DiscographyTracker.Data;
using DiscographyTracker.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscographyTracker.Services
{
    public class UserService
    {
        private readonly Guid _userId;

        public UserService(Guid userId)
        {
            _userId = userId;
        }

        public IEnumerable<UserArtistListItem> GetCrate()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Users
                        .FirstOrDefault(e => e.Id == _userId.ToString());
                var svc = CreateUserAlbumService();
                var crate = entity.UserArtists.Select(
                    e => new UserArtistListItem
                    {
                        ArtistName = e.Artist.ArtistName,
                        ArtistID = e.ArtistID,
                        UserArtistID = e.UserArtistID,
                        UserID = _userId.ToString(),
                        UserAlbums = svc.GetUserAlbums(e.ArtistID).ToList()
                    });

                return crate.ToList();
            }
        }
        private UserAlbumService CreateUserAlbumService()
        {
            var service = new UserAlbumService(_userId);
            return service;
        }
    }
}
