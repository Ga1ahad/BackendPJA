using Clothesy.Application.Persistence;
using Clothesy.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clothesy.Application.Clothes
{
    public class GetClothesFromSuitcase : IRequest<IEnumerable<Clothing>>
    {
        public int idClothing { get; set; }

        public class Handler : IRequestHandler<GetClothesFromSuitcase, IEnumerable<Clothing>>
        {
            private readonly IClothesyDb _context;
            public Handler(IClothesyDb context, IMediator mediator)
            {
                _context = context;
            }

            public async Task<IEnumerable<Clothing>> Handle(GetClothesFromSuitcase request, CancellationToken cancellationToken)
            {

                return await _context.Clothing
                               .Where(c => c.idClothing == request.idClothing)
                               .ToListAsync(cancellationToken: cancellationToken);
            }
        }
    }
}
