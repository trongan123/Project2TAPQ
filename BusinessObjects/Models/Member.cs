using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public partial class Member
    {
        [Key]
        public string IdMember { get; set; } = null!;
        public string IdUser { get; set; } = null!;
        public string IdRoom { get; set; } = null!;
        public DateTime Date { get; set; }
        public int? Status { get; set; }

        public virtual CooperativeRoom? IdRoomNavigation { get; set; } = null!;
        public virtual Account? IdUserNavigation { get; set; } = null!;
    }
}
