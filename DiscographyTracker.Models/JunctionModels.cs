using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscographyTracker.Models
{
    public class ArtistAlbumCreate
    {
        [Required]
        [Display(Name = "Artist Name")]
        public string ArtistName { get; set; }
        public int ArtistID { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "There are too many characters in this field.")]
        [Display(Name = "Album Title")]
        public string AlbumTitle { get; set; }
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:2020-01-01}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        public ArtistCreate ToArtistCreate(ArtistAlbumCreate model)
        {
            ArtistCreate entity = new ArtistCreate();
            entity.ArtistName = model.ArtistName;
            return entity;
        }

        public AlbumCreate ToAlbumCreate(ArtistAlbumCreate model, int id)
        {
            AlbumCreate entity = new AlbumCreate();
            entity.ArtistID = id;
            entity.AlbumTitle = model.AlbumTitle;
            entity.ReleaseDate = model.ReleaseDate;
            return entity;
        }
    }
}
