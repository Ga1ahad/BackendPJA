using System;
using System.Collections.Generic;

namespace Clothesy.ApiApp.Models
{
    public partial class ClothingColor
    {
        public int ClothingIdClothing { get; set; }
        public int ColorIdColor { get; set; }

        public virtual Clothing ClothingIdClothingNavigation { get; set; }
        public virtual Color ColorIdColorNavigation { get; set; }
    }
}
