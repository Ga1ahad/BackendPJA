using System;
using System.Collections.Generic;

namespace BackendApp.Models
{
    public partial class Suitcase
    {
        public Suitcase()
        {
            ClothingSuitcase = new HashSet<ClothingSuitcase>();
        }

        public int IdSuitcase { get; set; }
        public string Name { get; set; }
        public int IdUser { get; set; }
        public int IdTrip { get; set; }

        public virtual Trip IdTripNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
        public virtual ICollection<ClothingSuitcase> ClothingSuitcase { get; set; }
    }
}
