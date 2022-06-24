using System;
using System.Collections.Generic;

namespace CleaningService.Models
{
    public partial class CleanPricing
    {
        public CleanPricing()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public decimal PriceForM2 { get; set; }
        public TimeSpan Time { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}