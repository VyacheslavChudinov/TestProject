using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Entities.Abstract;

namespace Entities.Concrete
{
    public class Album : Identity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
