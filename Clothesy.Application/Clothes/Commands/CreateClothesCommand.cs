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
    public class CreateClothesCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int idClothingType { get; set; }
        public int idUser { get; set; }
        public string Tags { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string BucketName { get; set; }

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
                clothing.Url = request.Url;
                clothing.idClothingType = request.idClothingType;
                clothing.idUser = request.idUser;
                string tagReq = request.Tags;
                string[] tags = tagReq.Split(',');
                foreach (var tag in tags)
                {
                    clothing.ClothingTag.Add(new ClothingTag { idTag = Int32.Parse(tag), idClothing = clothing.idClothing });
                }
                _context.Clothing.Add(clothing);
                await _context.SaveChangesAsync(cancellationToken);
                return clothing.idClothing;
            }
        }
    }
}
