namespace Clothesy.Domain.Entities
{
    public partial class ClothingSuitcase
    {
        public int idSuitcase { get; set; }
        public int idClothing { get; set; }

        public virtual Clothing idClothingNavigation { get; set; }
        public virtual Suitcase idSuitcaseNavigation { get; set; }
    }
}
