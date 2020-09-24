using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscographyTracker.Data
{
    public class Artist
    {
        [Key]
        public int ArtistID { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "There are too many characters in this field.")]
        [Display(Name = "Artist Name")]
        public string ArtistName { get; set; }
        public virtual List<Album> Albums { get; set; }
    }
}
