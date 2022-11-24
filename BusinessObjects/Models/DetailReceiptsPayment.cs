using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public partial class DetailReceiptsPayment
    {
        [Key]
        public string IdDetailReceiptsPayments { get; set; } = null!;
        public string IdInvoice { get; set; } = null!;
        public string? NameStaff { get; set; }
        public double? Quanlity { get; set; }
        public decimal? Price { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }
        public DateTime? Date { get; set; }

        public virtual ReceiptsPayment? IdInvoiceNavigation { get; set; } = null!;
    }
}
