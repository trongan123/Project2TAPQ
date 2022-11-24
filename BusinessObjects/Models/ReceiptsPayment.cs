using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public partial class ReceiptsPayment
    {
        public ReceiptsPayment()
        {
            DetailReceiptsPayments = new HashSet<DetailReceiptsPayment>();
        }
        [Key]
        public string IdInvoice { get; set; } = null!;
        public string IdUser { get; set; } = null!;
        public decimal? Total { get; set; }
        public DateTime AddedDate { get; set; }
        public int Status { get; set; }

        public virtual Account? IdUserNavigation { get; set; } = null!;
        public virtual ICollection<DetailReceiptsPayment>? DetailReceiptsPayments { get; set; }
    }
}
