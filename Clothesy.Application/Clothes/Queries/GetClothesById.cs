﻿using Clothesy.Application.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clothesy.Application.Clothes.Queries
{
    public class GetClothesById : IRequest<List<ClothingDto>>
    {
        public int idClothing { get; set; }
        public int idUser { get; set; }


        public class Handler : IRequestHandler<GetClothesById, List<ClothingDto>>
        {
            private readonly IClothesyDb _context;
            public Handler(IClothesyDb context, IMediator mediator)
            {
                _context = context;
            }

            public async Task<List<ClothingDto>> Handle(GetClothesById request, CancellationToken cancellationToken)
            {
                List<ClothingDto> clothings = new List<ClothingDto>();
                clothings = await _context.Clothing
                               .Include(ct => ct.ClothingTag)
                               .ThenInclude(t => t.idTagNavigation)
                               .Where(u => u.idUser == request.idUser)
                               .Where(u => u.idClothing == request.idClothing)
                               .Select(c => new ClothingDto
                               {
                                   idClothing = c.idClothing,
                                   Name = c.Name,
                                   Tags = string.Join(",", c.ClothingTag.Select(o => o.idTagNavigation.TagName).ToArray()),
                                   Url = "https://clothesybucket.s3.eu-central-1.amazonaws.com/" + c.Url,
                                   ClothingTypeName = c.idClothingTypeNavigation.Name,
                                   idClothingType = c.idClothingTypeNavigation.idClothingType,
                               }
                               )
                               .ToListAsync(cancellationToken: cancellationToken);
                return clothings;
            }
        }
    }
}
