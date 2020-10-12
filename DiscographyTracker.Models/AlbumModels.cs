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
        public string ArtistName { get; set; }
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

    public class UserAlbumListItem
    {
        public int UserAlbumID { get; set; }
        public string AlbumTitle { get; set; }
        [UIHint("Favorited")]
        [Display(Name = "Favorited")]
        public bool IsFavorited { get; set; }
        [UIHint("Listened")]
        [Display(Name = "Listened")]
        public bool HaveListened { get; set; }
        public List<UserSongDetail> UserSongs { get; set; }
        public int AlbumFavoriteProgress
        {
            get
            {
                int k = 0;
                for (int i = 0; i < UserSongs.Count(); i++)
                {
                    if (UserSongs[i].IsFavorited)
                        k++;
                }
                if (k != 0)
                {
                    double ret = (double)k / (double)UserSongs.Count() * 100;
                    return Convert.ToInt32(ret);
                }
                else
                    return 0;
            }
        }
        public int HaveListenedProgress
        {
            get
            {
                int k = 0;
                for (int i = 0; i < UserSongs.Count(); i++)
                {
                    if (UserSongs[i].HaveListened)
                        k++;
                }
                if (k != 0)
                {
                    double ret = (double)k / (double)UserSongs.Count() * 100;
                    return Convert.ToInt32(ret);
                }

                else
                    return 0;
            }
        }
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
        public int UserArtistID { get; set; }
        public int UserAlbumID { get; set; }
        public string UserID { get; set; }
        public string AlbumTitle { get; set; }
        public int AlbumID { get; set; }
        public List<UserSongDetail> UserSongs { get; set; }
        public int SongCount
        {
            get
            {
                return UserSongs.Count();
            }
        }
        [UIHint("Favorited")]
        [Display(Name = "Favorited")]
        public bool IsFavorited { get; set; }
        [UIHint("Listened")]
        [Display(Name = "Listened")]
        public bool HaveListened { get; set; }
        public int AlbumFavoriteProgress
        {
            get
            {
                int k = 0;
                for (int i = 0; i < UserSongs.Count(); i++)
                {
                    if (UserSongs[i].IsFavorited)
                        k++;
                }
                if (k != 0)
                {
                    double ret = (double)k / (double)UserSongs.Count() * 100;
                    return Convert.ToInt32(ret);
                }
                else
                    return 0;
            }
        }
        public int HaveListenedProgress
        {
            get
            {
                int k = 0;
                for (int i = 0; i < UserSongs.Count(); i++)
                {
                    if (UserSongs[i].HaveListened)
                        k++;
                }
                if (k != 0)
                {
                    double ret = (double)k / (double)UserSongs.Count() * 100;
                    return Convert.ToInt32(ret);
                }
                else
                    return 0;
            }
        }
    }
}