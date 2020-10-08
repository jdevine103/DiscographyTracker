using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscographyTracker.Data
{
    public class UserArtist
    {
        [Key]
        public int UserArtistID { get; set; }

        [ForeignKey(nameof(User))]
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }

        [ForeignKey(nameof(Artist))]
        public int ArtistID { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual List<UserAlbum> UserAlbums {get; set;}
    }
    public class UserAlbum 
    {
        [Key]
        public int UserAlbumID { get; set; }

        [ForeignKey(nameof(User))]
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }

        [ForeignKey(nameof(Album))]
        public int AlbumID { get; set; }
        public virtual Album Album { get; set; }
       // [ForeignKey(nameof(UserArtist))]
       // public int UserArtistID { get; set; }
       //// public virtual UserArtist UserArtist { get; set; }
       // public virtual List<UserSong> UserSongs { get; set; }

        [DefaultValue(false)]
        public bool IsFavorited { get; set; }
        [DefaultValue(false)]
        public bool HaveListened { get; set; }
    }
    public class UserSong
    {
        [Key]
        public int UserSongID { get; set; }

        [ForeignKey(nameof(User))]
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }

        [ForeignKey(nameof(Song))]
        public int SongID { get; set; }
        public virtual Song Song { get; set; }
        //[ForeignKey(nameof(UserAlbum))]
        //public int UserAlbumID { get; set; }
        //public virtual UserAlbum UserAlbum { get; set; }
        [DefaultValue(false)]
        public bool IsFavorited { get; set; }
        [DefaultValue(false)]
        public bool HaveListened { get; set; }
    }
}
