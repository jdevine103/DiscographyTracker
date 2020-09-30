using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscographyTracker.Data
{
    public class Song
    {
        [Key]
        public int SongID { get; set; }
        [ForeignKey(nameof(Album))]
        public int AlbumID { get; set; }
        public virtual Album Album { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "There are too many characters in this field.")]
        [Display(Name = "Song Name")]
        public string SongName { get; set; }
        public bool HaveListened { get; set; }
    }
}
