using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public partial class PondDiary
    {
        [Key]
        public string IdDiary { get; set; } = null!;
        public string IdPond { get; set; } = null!;
        public double? Sanility { get; set; }
        public double? Ph { get; set; }
        public double? Temperature { get; set; }
        public double? WaterLevel { get; set; }
        public string? FishStatus { get; set; }
        public DateTime? Date { get; set; }

        public virtual Pond? IdPondNavigation { get; set; } = null!;
    }
}
