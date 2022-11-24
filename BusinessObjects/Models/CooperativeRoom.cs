using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace BusinessObjects.Models
{
    public partial class CooperativeRoom
    {
        public CooperativeRoom()
        {
            Members = new HashSet<Member>();
        }
        [Key]
        public string IdRoom { get; set; } = null!;
        public string IdCoo { get; set; } = null!;
        public string JoinCode { get; set; } = null!;
        public double? PondArea { get; set; }
        public int? Status { get; set; }

        public virtual Account? IdCooNavigation { get; set; } = null!;
        public virtual ICollection<Member>? Members { get; set; }
    }
}
