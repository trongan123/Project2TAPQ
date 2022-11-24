using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class District
    {
        public District()
        {
            Wards = new HashSet<Ward>();
        }
        [Key]
        public string IdDistrict { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string IdProvince { get; set; } = null!;
        public int? Status { get; set; }

        public virtual Province? IdProvinceNavigation { get; set; } = null!;
        public virtual ICollection<Ward>? Wards { get; set; }
    }
}
