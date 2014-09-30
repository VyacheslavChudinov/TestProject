using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Entities.Abstract;

namespace Entities.Concrete
{
    public class User: Identity
    {
        [Required(AllowEmptyStrings = false)]
        [MinLength(8)]
        [MaxLength(16)]
        public string Login { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MinLength(8)]
        [MaxLength(16)]                
        public string Password { get; set; }    
                
        public virtual ICollection<Song> Songs { get; set; }
        public virtual ICollection<Album> Albums { get; set; }
    }
}
