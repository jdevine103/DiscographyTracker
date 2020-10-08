﻿using DiscographyTracker.Data;
using DiscographyTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
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
            user.UserArtists.Add(new UserArtist { Artist = artist });
            user.UserAlbums.AddRange(artist.Albums.Select(a => new UserAlbum { AlbumID = a.AlbumID }));

            int songCount = 0;
            foreach(var album in artist.Albums)
            {
                user.UserSongs.AddRange(album.Songs.Select(a => new UserSong { SongID = a.SongID }));
                songCount += album.Songs.Count();
            }
                
            return ctx.SaveChanges() == 1 + artist.Albums.Count + songCount;
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
                    UserAlbums = GetUserAlbums(e.ArtistID)
                    });
                return crate.ToList();
        }

        public List<UserAlbumDetail> GetUserAlbums(int id)
        {
            var user = ctx.Users.FirstOrDefault(u => u.Id == _userId.ToString());

            var userAlbums = user.UserAlbums.Where(k => k.Album.ArtistID == id)
                        .Select(j => new UserAlbumDetail
                        {
                            AlbumTitle = j.Album.AlbumTitle,
                            IsFavorited = j.IsFavorited,
                            HaveListened = j.HaveListened,
                            UserSongs = GetUserSongs(j.AlbumID)
                        }).ToList();
            return userAlbums;
        }
        public List<UserSongDetail> GetUserSongs(int id)
        {
            var user = ctx.Users.FirstOrDefault(u => u.Id == _userId.ToString());

            var userSongs = user.UserSongs.Where(k => k.Song.AlbumID == id)
                        .Select(j => new UserSongDetail
                        {
                            SongName = j.Song.SongName,
                            IsFavorited = j.IsFavorited,
                            HaveListened = j.HaveListened,
                        }).ToList();
            return userSongs;
            //        UserSongs = e.User.UserSongs.Where(i => i.UserAlbumID == j.UserAlbumID)
            //            .Select(q => new UserSongDetail
            //            {
            //                UserAlbumID = j.UserAlbumID,
            //                IsFavorited = j.IsFavorited,
            //                HaveListened = j.HaveListened
            //            })
            //    }).ToList()
        }
    }
}
