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
    public class UserCrate
    {
        [Key]
        public int CrateID { get; set; }
        [ForeignKey(nameof(User))]
        public int UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        //i dont think this works. come back later
        public virtual List<UserArtist> UserArtists { get; set; }      
        
        
        //public virtual List<UserAlbum> UserAlbums { get; set; }      
        //public virtual List<UserSongs> UserSongs { get; set; }      
        
    }
}