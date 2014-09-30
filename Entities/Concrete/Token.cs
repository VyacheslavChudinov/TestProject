using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Abstract;

namespace Entities.Concrete
{
    public class Token : Identity
    {
        public string Value { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime ExpireDate { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        
        public virtual User User { get; set; }

        
    }
}