using System;
using System.Collections.Generic;

namespace CleaningService.Models
{
    public partial class PayList
    {
        public PayList()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public decimal Sum { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
