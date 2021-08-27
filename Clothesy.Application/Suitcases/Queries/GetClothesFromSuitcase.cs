using Clothesy.Application.Clothes.Queries;
using Clothesy.Application.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clothesy.Application.Suitcases.Commands
{
    public class GetClothesFromSuitcase : IRequest<List<ClothingDto>>
    {
        public int idClothing { get; set; }
        public int idUser { get; set; }
        public int idTrip { get; set; }


        public class Handler : IRequestHandler<GetClothesFromSuitcase, List<ClothingDto>>
        {
            private readonly IClothesyDb _context;
            public Handler(IClothesyDb context, IMediator mediator)
            {
                _context = context;
            }

            public async Task<List<ClothingDto>> Handle(GetClothesFromSuitcase request, CancellationToken cancellationToken)
            {
                //    var clothes = new List<IQueryable>();
                //    var clothingSuitcaseData = _context.ClothingSuitcase.Where(s => s.idSuitcase == request.idSuitcase);

                //    foreach (var idClothingIterator in clothingSuitcaseData)
                //    {
                //        clothes.Add(_context.Clothing.Where(c => c.idClothing == idClothingIterator.idClothing));
                //    }
                var suitcase = _context.Suitcase
                .Where(b => b.idTrip == request.idTrip)
                .FirstOrDefault();

                List<ClothingDto> clothings = new List<ClothingDto>();

                clothings = await _context.Clothing
                               .Include(cs => cs.ClothingSuitcase)
                               .Select(c => new ClothingDto
                               {
                                   idClothing = c.idClothing,
                                   Name = c.Name,
                                   Url = "https://clothesybucket.s3.eu-central-1.amazonaws.com/" + c.Url,
                                   ClothingTypeName = c.idClothingTypeNavigation.Name,
                                   Suitcases = c.ClothingSuitcase.Select(o => o.idSuitcaseNavigation.idSuitcase).ToArray(),
                               }
                               )
                               .Where(x => x.Suitcases.Contains(suitcase.idSuitcase))
                               .ToListAsync(cancellationToken: cancellationToken);

                return clothings;
            }
        }
    }
}

