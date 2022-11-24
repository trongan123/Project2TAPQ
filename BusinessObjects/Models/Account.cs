using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public partial class Account
    {
        public Account()
        {
            CooperativeRooms = new HashSet<CooperativeRoom>();
            HistoryStoreHouses = new HashSet<HistoryStoreHouse>();
            Members = new HashSet<Member>();
            Notifies = new HashSet<Notify>();
            Ponds = new HashSet<Pond>();
            QuantityHouses = new HashSet<QuantityHouse>();
            ReceiptsPayments = new HashSet<ReceiptsPayment>();
            StoreHouses = new HashSet<StoreHouse>();
        }
        [Key]
        public string IdAcc { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime? Birthday { get; set; }
        public string Address { get; set; } = null!;
        public int Role { get; set; }
        public string? IdRoleStaff { get; set; }
        public string IdWard { get; set; } = null!;
        public DateTime DateJoin { get; set; }
        public string? Image { get; set; }
        public int? Status { get; set; }

        public virtual RoleStaff? IdRoleStaffNavigation { get; set; }
        public virtual Ward? IdWardNavigation { get; set; } = null!;
        public virtual ICollection<CooperativeRoom>? CooperativeRooms { get; set; }
        public virtual ICollection<HistoryStoreHouse>? HistoryStoreHouses { get; set; }
        public virtual ICollection<Member>? Members { get; set; }
        public virtual ICollection<Notify>? Notifies { get; set; }
        public virtual ICollection<Pond>? Ponds { get; set; }
        public virtual ICollection<QuantityHouse>? QuantityHouses { get; set; }
        public virtual ICollection<ReceiptsPayment>? ReceiptsPayments { get; set; }
        public virtual ICollection<StoreHouse>? StoreHouses { get; set; }
    }
}
