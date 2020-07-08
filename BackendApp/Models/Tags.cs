using System;
using System.Collections.Generic;

namespace Clothesy.ApiApp.Models
{
    public partial class Tags
    {
        public Tags()
        {
            ClothingTag = new HashSet<ClothingTag>();
        }

        public int IdTag { get; set; }
        public string TagName { get; set; }

        public virtual ICollection<ClothingTag> ClothingTag { get; set; }
    }
}
