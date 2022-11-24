using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public partial class Province
    {
        public Province()
        {
            Districts = new HashSet<District>();
        }
        [Key]
        public string IdProvince { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public int? Status { get; set; }

        public virtual ICollection<District> Districts { get; set; }
    }
}
