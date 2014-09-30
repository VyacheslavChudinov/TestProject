using System.ComponentModel.DataAnnotations;
using Entities.Abstract;

namespace Entities.Concrete
{
    public class Song : Identity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public virtual Album Album { get; set; }
    }
}
