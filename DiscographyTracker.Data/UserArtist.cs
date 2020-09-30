using System;
using System.Collections.Generic;
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
    }
}
