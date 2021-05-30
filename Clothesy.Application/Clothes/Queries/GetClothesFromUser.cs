using Clothesy.Application.Persistence;
using Clothesy.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clothesy.Application.Clothes.Queries
{
    public class GetClothesFromUser : IRequest<List<ClothingDto>>
    {
        public int idClothing { get; set; }
        public int idUser { get; set; }


        public class Handler : IRequestHandler<GetClothesFromUser, List<ClothingDto>>
        {
            private readonly IClothesyDb _context;
            public Handler(IClothesyDb context, IMediator mediator)
            {
                _context = context;
            }

            public async Task<List<ClothingDto>> Handle(GetClothesFromUser request, CancellationToken cancellationToken)
            {
                List<ClothingDto> clothings = new List<ClothingDto>();
                clothings = await _context.Clothing
                               .Include(ct => ct.ClothingTag)
                               .ThenInclude(t => t.idTagNavigation)
                               .Where(u => u.idUser == request.idUser)
                               .Select(c => new ClothingDto
                               {
                                   Name = c.Name,
                                   Tags = c.ClothingTag.Select(o => o.idTagNavigation.TagName),
                                   Url = "https://clothesybucket.s3.eu-central-1.amazonaws.com/" + c.Url,
                                   ClothingTypeName = c.idClothingTypeNavigation.Name,

                               }
                               )
                               .ToListAsync(cancellationToken: cancellationToken);




                return clothings;
            }
        }
    }
}
