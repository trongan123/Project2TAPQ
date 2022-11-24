using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public partial class StoreHouse
    {
        public StoreHouse()
        {
            HistoryStoreHouses = new HashSet<HistoryStoreHouse>();
            ItemStoreHouses = new HashSet<ItemStoreHouse>();
        }
        [Key]
        public string IdSHouse { get; set; } = null!;
        public string IdUser { get; set; } = null!;
        public int? Status { get; set; }

        public virtual Account? IdUserNavigation { get; set; } = null!;
        public virtual ICollection<HistoryStoreHouse>? HistoryStoreHouses { get; set; }
        public virtual ICollection<ItemStoreHouse>? ItemStoreHouses { get; set; }
    }
}
