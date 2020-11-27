using System.Collections.Generic;

namespace Clothesy.Domain.Entities
{
    public partial class Tags
    {
        public Tags()
        {
            ClothingTag = new HashSet<ClothingTag>();
        }

        public int idTag { get; set; }
        public string TagName { get; set; }

        public virtual ICollection<ClothingTag> ClothingTag { get; set; }
    }
}
