using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class ItemCategory
    {
        public ItemCategory()
        {
            HistoryStoreHouses = new HashSet<HistoryStoreHouse>();
            ItemStoreHouses = new HashSet<ItemStoreHouse>();
        }
        [Key]
        public string IdItemCategory { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int? Status { get; set; }

        public virtual ICollection<HistoryStoreHouse>? HistoryStoreHouses { get; set; }
        public virtual ICollection<ItemStoreHouse>? ItemStoreHouses { get; set; }
    }
}
