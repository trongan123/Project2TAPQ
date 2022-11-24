using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public partial class RoleStaff
    {
        public RoleStaff()
        {
            Accounts = new HashSet<Account>();
        }
        [Key]
        public string IdRoleStaff { get; set; } = null!;
        public string IdAcc { get; set; } = null!;
        public string IdRole { get; set; } = null!;
        public decimal? Salary { get; set; }
        public int? Status { get; set; }

        public virtual Role? IdRoleNavigation { get; set; } = null!;
        public virtual ICollection<Account>? Accounts { get; set; }
    }
}
