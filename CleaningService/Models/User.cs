using System;
using System.Collections.Generic;

namespace CleaningService.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int IdRole { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual Role IdRoleNavigation { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
