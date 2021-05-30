using Clothesy.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Clothesy.Application.Clothes.Queries
{
    public class ClothingDto
    {


        public string Name { get; set; }
        public string ClothingTypeName { get; set; }

        public IEnumerable<string> Tags { get; internal set; }
        public string Url { get; set; }
    }
}
