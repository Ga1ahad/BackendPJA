using System.Collections.Generic;

namespace Clothesy.Domain.Entities
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
