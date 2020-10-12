using DiscographyTracker.Data;
using DiscographyTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace DiscographyTracker.Services
{
    public class CrateService
    {
        readonly ApplicationDbContext ctx = new ApplicationDbContext();
        private readonly Guid _userId;

        public CrateService(Guid userId)
        {
            _userId = userId;
        }
        public bool AddToCrate(int id)
        {
            var user = ctx.Users.FirstOrDefault(u => u.Id == _userId.ToString());
            var artist = ctx.Artists.Find(id);
            //add if for duplicates
            UserArtist userArtist = new UserArtist { Artist = artist };
            user.UserArtists.Add(userArtist);

            ctx.SaveChanges();

            user.UserAlbums.AddRange(artist.Albums.Select(a => new UserAlbum { AlbumID = a.AlbumID, UserArtistID = userArtist.UserArtistID}));

            ctx.SaveChanges();

            int songCount = 0;
            foreach(var album in artist.Albums)
            {
                var userAlbum = ctx.UserAlbums.FirstOrDefault(j => j.AlbumID == album.AlbumID && j.UserID == user.Id);
                user.UserSongs.AddRange(album.Songs.Select(a => new UserSong { SongID = a.SongID, UserAlbumID = userAlbum.UserAlbumID }));
                songCount += album.Songs.Count();
            }
                
            return ctx.SaveChanges() == songCount;
        }
        public IEnumerable<UserArtistListItem> GetCrate()
        {
            var user = ctx.Users.FirstOrDefault(u => u.Id == _userId.ToString());

            IEnumerable<UserArtistListItem> crate = user.UserArtists.Select(
                e => new UserArtistListItem
                {
                    ArtistName = e.Artist.ArtistName,
                    ArtistID = e.ArtistID,
                    UserArtistID = e.UserArtistID,
                    UserID = _userId.ToString(),
                    UserAlbums = GetUserAlbums(e.UserArtistID)
                    });
                return crate.ToList();
        }

        public List<UserAlbumDetail> GetUserAlbums(int id)
        {
            var user = ctx.Users.FirstOrDefault(u => u.Id == _userId.ToString());

            var userAlbums = user.UserAlbums.Where(k => k.UserArtistID == id)
                        .Select(j => new UserAlbumDetail
                        {
                            UserAlbumID = j.UserAlbumID,
                            AlbumTitle = j.Album.AlbumTitle,
                            IsFavorited = j.IsFavorited,
                            HaveListened = j.HaveListened,
                            UserSongs = GetUserSongs(j.UserAlbumID)
                        }).ToList();
            return userAlbums;
        }
        public List<UserSongDetail> GetUserSongs(int id)
        {
            var user = ctx.Users.FirstOrDefault(u => u.Id == _userId.ToString());

            var userSongs = user.UserSongs.Where(k => k.UserAlbumID == id)
                        .Select(j => new UserSongDetail
                        {
                            UserSongID = j.UserSongID,
                            SongName = j.Song.SongName,
                            IsFavorited = j.IsFavorited,
                            HaveListened = j.HaveListened,
                        }).ToList();
            return userSongs;
        }
    }
}
