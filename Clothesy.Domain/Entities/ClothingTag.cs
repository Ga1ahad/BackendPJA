namespace Clothesy.Domain.Entities
{
    public partial class ClothingTag
    {
        public int idClothing { get; set; }
        public int idTag { get; set; }

        public virtual Clothing idClothingNavigation { get; set; }
        public virtual Tags idTagNavigation { get; set; }
    }
}
