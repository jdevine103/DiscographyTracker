using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscographyTracker.Models
{
    public class SongListItem
    {
        public int SongID { get; set; }
        public int AlbumID { get; set; }
        [Display(Name = "Song Name")]
        public string SongName { get; set; }
        [Display(Name = "Listened?")]
        public bool HaveListened { get; set; }
    }
    public class SongCreate
    {
        public int AlbumID { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "There are too many characters in this field.")]
        [Display(Name = "Song Name")]
        public string SongName { get; set; }
        public bool HaveListened { get; set; }
    }
    public class SongEdit
    {
        public int SongID { get; set; }
        public int AlbumID { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "There are too many characters in this field.")]
        [Display(Name = "Song Name")]
        public string SongName { get; set; }
        public bool HaveListened { get; set; }
    }
    public class SongDetail
    {
        public int SongID { get; set; }
        public int AlbumID { get; set; }
        public string ArtistName { get; set; }
        public string AlbumTitle { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "There are too many characters in this field.")]
        [Display(Name = "Song Name")]
        public string SongName { get; set; }
        public bool HaveListened { get; set; }
    }
    public class UserSongListItem
    {
        public int UserAlbumID { get; set; }

        public string UserID { get; set; }

        public int SongID { get; set; }
        public bool IsFavorited { get; set; }
        public bool HaveListened { get; set; }
    }    
    public class UserSongCreate
    {
        public int UserAlbumID { get; set; }

        public string UserID { get; set; }

        public int SongID { get; set; }
        public bool IsFavorited { get; set; }
        public bool HaveListened { get; set; }
    }    
    public class UserSongEdit
    {
        public int UserAlbumID { get; set; }

        public string UserID { get; set; }

        public int SongID { get; set; }
        public bool IsFavorited { get; set; }
        public bool HaveListened { get; set; }
    }    
    public class UserSongDetail
    {
        public int UserAlbumID { get; set; }

        public string UserID { get; set; }

        public int SongID { get; set; }
        public bool IsFavorited { get; set; }
        public bool HaveListened { get; set; }
    }
}
