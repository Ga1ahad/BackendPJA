using Clothesy.Application.Persistence;
using Clothesy.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Clothesy.Application.Clothes.Commands
{
    public class UpdateClothesCommand : IRequest<int>
    {

        public string Name { get; set; }
        public string Url { get; set; }
        public int idClothingType { get; set; }
        public int idUser { get; set; }
        public string Tags { get; set; }
        public int idClothing { get; set; }

        public class UpdateClothesCommandHandler : IRequestHandler<UpdateClothesCommand, int>
        {
            private readonly IClothesyDb _context;
            public UpdateClothesCommandHandler(IClothesyDb context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpdateClothesCommand request, CancellationToken cancellationToken)
            {
                //var clothing = new Clothing();
                var clothing = _context.Clothing.Single(a => a.idClothing == request.idClothing);
                var old_tags = _context.ClothingTag.Where(c => c.idClothing == request.idClothing);
                clothing.Name = request.Name;
                clothing.idClothingType = request.idClothingType;
                string tagReq = request.Tags;
                string[] tags = tagReq.Split(',');
                foreach (var book in old_tags)
                {
                    clothing.ClothingTag.Remove(book);
                }
                //    var toRemoveModels = _context.ClothingTag
                //                          .Where(c => c.idClothing == request.idClothing)
                //                           .ToList();
                //  clothing.ClothingTag.Clear(new ClothingTag { idClothing = clothing.idClothing });
                foreach (var tag in tags)
                {
                    clothing.ClothingTag.Add(new ClothingTag { idTag = Int32.Parse(tag), idClothing = clothing.idClothing });
                }
                _context.Clothing.Update(clothing);
                await _context.SaveChangesAsync(cancellationToken);
                return clothing.idClothing;
            }
        }
    }
}
