using System.Collections.Generic;

namespace Clothesy.Domain.Entities
{
    public partial class Suitcase
    {
        public Suitcase()
        {
            ClothingSuitcase = new HashSet<ClothingSuitcase>();
        }

        public int idSuitcase { get; set; }
        public string Name { get; set; }
        public int idTrip { get; set; }

        public virtual Trip idTripNavigation { get; set; }
        public virtual ICollection<ClothingSuitcase> ClothingSuitcase { get; set; }
    }
}
