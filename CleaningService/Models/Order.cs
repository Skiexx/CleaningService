using System;
using System.Collections.Generic;

namespace CleaningService.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public int IdCleanPrice { get; set; }
        public int IdUser { get; set; }
        public int CleanSizeM2 { get; set; }
        public int IdPayList { get; set; }

        public virtual CleanPricing IdCleanPriceNavigation { get; set; } = null!;
        public virtual PayList IdPayListNavigation { get; set; } = null!;
        public virtual User IdUserNavigation { get; set; } = null!;
    }
}
