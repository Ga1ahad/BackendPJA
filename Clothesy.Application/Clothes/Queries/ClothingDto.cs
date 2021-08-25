using Clothesy.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Clothesy.Application.Clothes.Queries
{
    public class ClothingDto
    {
        public int idClothing { get; set; }
        public string Name { get; set; }
        public string ClothingTypeName { get; set; }
        public string Url { get; set; }
        public string Tags { get; internal set; }
        public int[] x { get; internal set; }
    }
}
