using System;
using System.Collections.Generic;

namespace Clothesy.Domain.Entities
{
    public partial class Suitcase
    {
        public Suitcase()
        {
            ClothingSuitcase = new HashSet<ClothingSuitcase>();
        }

        public int IdSuitcase { get; set; }
        public string Name { get; set; }
        public int IdTrip { get; set; }

        public virtual Trip IdTripNavigation { get; set; }
        public virtual ICollection<ClothingSuitcase> ClothingSuitcase { get; set; }
    }
}
