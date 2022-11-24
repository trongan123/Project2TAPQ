using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public partial class QuantityHouse
    {
        [Key]
        public string IdQuantity { get; set; } = null!;
        public string IdAcc { get; set; } = null!;
        public double Quanlity { get; set; }
        public DateTime? AddedDate { get; set; }

        public virtual Account IdAccNavigation { get; set; } = null!;
    }
}
