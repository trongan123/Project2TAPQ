using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public partial class Pond
    {
        public Pond()
        {
            PondDiaries = new HashSet<PondDiary>();
        }
        [Key]
        public string IdPond { get; set; } = null!;
        public string IdAcc { get; set; } = null!;
        public string IdFcategory { get; set; } = null!;
        public string Name { get; set; } = null!;
        public double? PondArea { get; set; }
        public string? Image { get; set; }
        public string? Session { get; set; }
        public double? QuantityOfFingerlings { get; set; }
        public double? QuanlityOfEnd { get; set; }
        public DateTime? StartDay { get; set; }
        public DateTime? EndDay { get; set; }
        public int? Status { get; set; }

        public virtual Account? IdAccNavigation { get; set; } = null!;
        public virtual FishCategory? IdFcategoryNavigation { get; set; } = null!;
        public virtual ICollection<PondDiary>? PondDiaries { get; set; }
    }
}
