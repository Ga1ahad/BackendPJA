    using System;
using System.Collections.Generic;

namespace BackendApp.Models
{
    public partial class ClothingPicture
    {
        public int IdClothingPicture { get; set; }
        public string Title { get; set; }
        public byte[] ClothImg { get; set; }
        public int IdClothin { get; set; }

        public virtual Clothing IdClothinNavigation { get; set; }
    }
}
