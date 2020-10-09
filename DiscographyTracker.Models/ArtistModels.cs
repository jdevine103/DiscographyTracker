using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscographyTracker.Models
{
    public class ArtistListItem
    {
        public int ArtistID { get; set; }
        [Display(Name = "Artist Name")]
        public string ArtistName { get; set; }
        public IEnumerable<AlbumDetail> Albums { get; set; }
        public int? AlbumCount
        {
            get
            {
                return Albums.Count();
            }
        }
    }
    public class ArtistCreate
    {
        [Required]
        [Display(Name = "Artist Name")]
        public string ArtistName { get; set; }
    }
    public class ArtistEdit
    {
        [Required]
        public int ArtistID { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "There are too many characters in this field.")]
        [Display(Name = "Artist Name")]
        public string ArtistName { get; set; }
    }
    public class ArtistDetail
    {
        [Required]
        public int ArtistID { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "There are too many characters in this field.")]
        [Display(Name = "Artist Name")]
        public string ArtistName { get; set; }
        public IEnumerable<AlbumDetail> Albums { get; set; }
        public int AlbumCount
        {
            get
            {
                return Albums.Count();
            }
        }
        public UserArtistCreate ToUserArtistCreate()
        {
            var userArtistCreate = new UserArtistCreate();
            userArtistCreate.ArtistID = ArtistID;
            return userArtistCreate;
        }
    }
    public class UserArtistCreate
    {
        public string UserID { get; set; }
        public int UserArtistID { get; set; }
        [Required]
        public int ArtistID { get; set; }
    }
    public class UserArtistListItem
    {
        public string ArtistName {get; set; }
        public string UserID { get; set; }
        public int UserArtistID { get; set; }
        public int ArtistID { get; set; }
        public List<UserAlbumDetail> UserAlbums { get; set; }
        public int AlbumFavoriteProgress
        {
            get
            {
                int k = 0;
                for (int i = 0; i < UserAlbums.Count(); i++)
                {
                    if (UserAlbums[i].IsFavorited)
                        k++;
                }
                if (k != 0)
                {
                    double ret = (double)k / (double)UserAlbums.Count() * 100;
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
                for (int i = 0; i < UserAlbums.Count(); i++)
                {
                    if (UserAlbums[i].HaveListened)
                        k++;
                }
                if (k != 0)
                {
                    double ret = (double)k / (double)UserAlbums.Count() * 100;
                    return Convert.ToInt32(ret);
                }
                else
                    return 0;
            }
        }
    }
    public class UserArtistEdit
    {
        public string UserID { get; set; }
        public int UserArtistID { get; set; }
        public int ArtistID { get; set; }
    }
    public class UserArtistDetail
    {
        public string UserID { get; set; }
        public int UserArtistID { get; set; }
        public int ArtistID { get; set; }
        public List<UserAlbumDetail> UserAlbums { get; set; }
    }

}
