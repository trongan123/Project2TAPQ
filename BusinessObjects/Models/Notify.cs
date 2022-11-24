using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public partial class Notify
    {
        [Key]
        public string IdNotify { get; set; } = null!;
        public string IdAcc { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public int? Status { get; set; }
        public string? Name { get; set; }

        public virtual Account? IdAccNavigation { get; set; } = null!;
    }
}
