﻿using System;
using System.Collections.Generic;

namespace Clothesy.ApiApp.Models
{
    public partial class Color
    {
        public Color()
        {
            ClothingColor = new HashSet<ClothingColor>();
        }

        public int IdColor { get; set; }
        public string ColorName { get; set; }

        public virtual ICollection<ClothingColor> ClothingColor { get; set; }
    }
}
