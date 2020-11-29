using Clothesy.Application.Persistence;
using Clothesy.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Clothesy.Application.Clothes.Commands
{
    public class CreateClothesCommand : IRequest<int>
    {
        public string Name { get; set; }
        public int idClothingType { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int idBodyPart { get; set; }
        public int idUser { get; set; }

        public virtual BodyPart idBodyPartNavigation { get; set; }
        public virtual ClothingType idClothingTypeNavigation { get; set; }
        public virtual ICollection<ClothingColor> ClothingColor { get; set; }
        public virtual ICollection<ClothingPicture> ClothingPicture { get; set; }
        public virtual ICollection<ClothingTag> ClothingTag { get; set; }

        public class CreateClothesCommandHandler : IRequestHandler<CreateClothesCommand, int>
        {
            private readonly IClothesyDb _context;
            public CreateClothesCommandHandler(IClothesyDb context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateClothesCommand request, CancellationToken cancellationToken)
            {
                var clothing = new Clothing();
                clothing.Name = request.Name;
                clothing.Description = request.Description;
                clothing.CreatedAt = request.CreatedAt;
                clothing.idBodyPart = request.idBodyPart;
                clothing.idUser = request.idUser;
                clothing.ClothingColor = request.ClothingColor;
                clothing.ClothingPicture = request.ClothingPicture;
                clothing.ClothingTag = request.ClothingTag;
                _context.Clothing.Add(clothing);
                await _context.SaveChangesAsync(cancellationToken);
                return clothing.idClothing;
            }
        }
    }
}
