using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public partial class Role
    {
        public Role()
        {
            RoleStaffs = new HashSet<RoleStaff>();
        }
        [Key]
        public string IdRole { get; set; } = null!;
        public string Type { get; set; } = null!;
        public int? Status { get; set; }

        public virtual ICollection<RoleStaff> RoleStaffs { get; set; }
    }
}
