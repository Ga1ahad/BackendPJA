using System;
using System.Collections.Generic;

namespace BackendApp.Models
{
    public partial class BodyPart
    {
        public BodyPart()
        {
            Clothing = new HashSet<Clothing>();
        }

        public int IdBodyPart { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Clothing> Clothing { get; set; }
    }
}
