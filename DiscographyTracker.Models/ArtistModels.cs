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
    }
    public class ArtistCreate
    {
        [Required]
        [Display(Name = "Artist Name")]
        public string ArtistName { get; set; }
    }
}
