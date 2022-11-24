using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public partial class ItemStoreHouse
    {
        [Key]
        public string IdItemStoreHouse { get; set; } = null!;
        public string IdSHouse { get; set; } = null!;
        public string Name { get; set; } = null!;
        public double Quanlity { get; set; }
        public string IdItemCategory { get; set; } = null!;
        public string? Note { get; set; }

        public virtual ItemCategory? IdItemCategoryNavigation { get; set; } = null!;
        public virtual StoreHouse? IdSHouseNavigation { get; set; } = null!;
    }
}
