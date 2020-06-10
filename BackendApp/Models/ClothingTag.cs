using System;
using System.Collections.Generic;

namespace BackendApp.Models
{
    public partial class ClothingTag
    {
        public int IdClothing { get; set; }
        public int IdTag { get; set; }

        public virtual Clothing IdClothingNavigation { get; set; }
        public virtual Tags IdTagNavigation { get; set; }
    }
}
