using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public partial class FishCategory
    {
        public FishCategory()
        {
            Ponds = new HashSet<Pond>();
        }
        [Key]
        public string IdFcategory { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public string? Image { get; set; }
        public int HarvestTime { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }
        public double? Ph { get; set; }
        public double? Temperature { get; set; }
        public double? WaterLevel { get; set; }
        public double? Sanility { get; set; }

        public virtual ICollection<Pond>? Ponds { get; set; }
    }
}
