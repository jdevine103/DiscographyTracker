using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscographyTracker.Models
{
    public class AlbumListItem
    {
        public int AlbumID { get; set; }
        public int ArtistID { get; set; }
        [Display(Name = "Album Title")]
        public string AlbumTitle { get; set; }
        [Display(Name = "Release Date")]
        public DateTimeOffset ReleaseDate { get; set; }
    }
    public class AlbumCreate
    {
        public int AlbumID { get; set; }
        public int ArtistID { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "There are too many characters in this field.")]
        [Display(Name = "Album Title")]
        public string AlbumTitle { get; set; }
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:2020-01-01}", ApplyFormatInEditMode = true)]
        public DateTimeOffset ReleaseDate { get; set; }

        public AlbumEdit ToAlbumEdit()
        {
            AlbumEdit editModel = new AlbumEdit();

            editModel.AlbumID = AlbumID;
            editModel.ArtistID = ArtistID;
            editModel.AlbumTitle = AlbumTitle;
            editModel.ReleaseDate = ReleaseDate;

            return editModel;
        }
    }
    public class AlbumEdit
    {
        public int AlbumID { get; set; }
        public int ArtistID { get; set; }
        [MaxLength(200, ErrorMessage = "There are too many characters in this field.")]
        [Display(Name = "Album Title")]
        public string AlbumTitle { get; set; }
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTimeOffset ReleaseDate { get; set; }
    }
    public class AlbumDetail
    {
        public int AlbumID { get; set; }
        public int ArtistID { get; set; }
        [Display(Name = "Artist Name")]
        public string ArtistName { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "There are too many characters in this field.")]
        [Display(Name = "Album Title")]
        public string AlbumTitle { get; set; }
        [Display(Name = "Release Date")]
        public DateTimeOffset ReleaseDate { get; set; }
        public IEnumerable<SongDetail> Songs { get; set; }
    }
    public class UserAlbumCreate
    {
        public int UserAlbumID { get; set; }
        public string UserID { get; set; }
        public int AlbumID { get; set; }
        public bool IsFavorited { get; set; }
        public bool HaveListened { get; set; }
    }
        public class UserAlbumListItem
    {
        public int UserAlbumID { get; set; }
        public string UserID { get; set; }
        public int AlbumID { get; set; }
        public bool IsFavorited { get; set; }
        public bool HaveListened { get; set; }
    }
        public class UserAlbumEdit
    {
        public int UserAlbumID { get; set; }
        public string UserID { get; set; }
        public int AlbumID { get; set; }
        public bool IsFavorited { get; set; }
        public bool HaveListened { get; set; }
    }
        public class UserAlbumDetail
    {
        public int UserAlbumID { get; set; }
        public string UserID { get; set; }
        public int AlbumID { get; set; }
        public bool IsFavorited { get; set; }
        public bool HaveListened { get; set; }
    }

}
