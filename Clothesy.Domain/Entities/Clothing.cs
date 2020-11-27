using System;
using System.Collections.Generic;

namespace Clothesy.Domain.Entities
{
    public partial class Clothing
    {
        public Clothing()
        {
            ClothingColor = new HashSet<ClothingColor>();
            ClothingPicture = new HashSet<ClothingPicture>();
            ClothingSuitcase = new HashSet<ClothingSuitcase>();
            ClothingTag = new HashSet<ClothingTag>();
        }

        public int idClothing { get; set; }
        public string Name { get; set; }
        public int idClothingType { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int idBodyPart { get; set; }
        public int idUser { get; set; }

        public virtual BodyPart idBodyPartNavigation { get; set; }
        public virtual ClothingType idClothingTypeNavigation { get; set; }
        public virtual User idUserNavigation { get; set; }
        public virtual ICollection<ClothingColor> ClothingColor { get; set; }
        public virtual ICollection<ClothingPicture> ClothingPicture { get; set; }
        public virtual ICollection<ClothingSuitcase> ClothingSuitcase { get; set; }
        public virtual ICollection<ClothingTag> ClothingTag { get; set; }
    }
}
