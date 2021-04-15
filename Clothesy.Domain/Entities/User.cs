using System;
using System.Collections.Generic;

namespace Clothesy.Domain.Entities
{
    public partial class User
    {
        public User()
        {
            Clothing = new HashSet<Clothing>();
            Trip = new HashSet<Trip>();
            this.CreatedAt = DateTime.UtcNow;
            this.LastLogin = DateTime.UtcNow;
        }

        public int idUser { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public DateTime LastLogin { get; set; }

        public virtual ICollection<Clothing> Clothing { get; set; }
        public virtual ICollection<Trip> Trip { get; set; }

        public static int FindFirstValue(object nameIdentifier)
        {
            throw new NotImplementedException();
        }
    }
}
