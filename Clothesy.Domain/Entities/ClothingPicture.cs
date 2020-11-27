namespace Clothesy.Domain.Entities
{
    public partial class ClothingPicture
    {
        public int idClothingPicture { get; set; }
        public string Title { get; set; }
        public string ClothingUrl { get; set; }
        public int idClothin { get; set; }

        public virtual Clothing idClothinNavigation { get; set; }
    }
}
