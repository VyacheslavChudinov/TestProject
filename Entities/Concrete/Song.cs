using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Abstract;

namespace Entities.Concrete
{
    public class Song : Identity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Duration { get; set; }

        [ForeignKey("Downloader")]
        public int DownloaderId { get; set; }

        [Required]
        public virtual User Downloader { get; set; }

        [ForeignKey("Album")]
        public int AlbumId { get; set; }

        [Required]
        public virtual Album Album { get; set; }

        public virtual ICollection<SongDownloadStatistics> SongDownloadStatisticses { get; set; }
    }
}
