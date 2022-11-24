using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public partial class Ward
    {
        public Ward()
        {
            Accounts = new HashSet<Account>();
        }
        [Key]
        public string IdWard { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string IdDistrict { get; set; } = null!;
        public int? Status { get; set; }

        public virtual District IdDistrictNavigation { get; set; } = null!;
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
