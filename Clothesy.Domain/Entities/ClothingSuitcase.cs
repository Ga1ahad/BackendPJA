namespace Clothesy.Domain.Entities
{
    public partial class ClothingSuitcase
    {
        public int IdSuitcase { get; set; }
        public int IdClothing { get; set; }

        public virtual Clothing IdClothingNavigation { get; set; }
        public virtual Suitcase IdSuitcaseNavigation { get; set; }
    }
}
