using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Abstract;

namespace Entities.Concrete
{
    public class SongDownloadStatistics : Identity
    {
        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime DownloadTime { get; set; }

        [ForeignKey("Song")]
        public int SongId { get; set; }
        public virtual Song Song { get; set; }

    }
}
