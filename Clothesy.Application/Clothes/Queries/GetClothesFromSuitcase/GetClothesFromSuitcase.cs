using MediatR;
using System.Collections.Generic;
using Clothesy.Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using Clothesy.Application.Persistence;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Clothesy.Application.Clothes.Queries.GetClothesFromSuitcase
{
    public class GetClothesFromSuitcase : IRequest<IEnumerable<Clothing>>
    {
        public int IdUser { get; set; }

        public class Handler : IRequestHandler<GetClothesFromSuitcase, IEnumerable<Clothing>>
        {
            private readonly IClothesyDb _context;
            private readonly IMediator _mediator;
            public Handler(IClothesyDb context, IMediator mediator)
            {
                _context = context;
            }

            public async Task<IEnumerable<Clothing>> Handle(GetClothesFromSuitcase request, CancellationToken cancellationToken)
            {
                return await _context.Clothing
                               .Where(c => c.IdUser == request.IdUser)
                               .OrderByDescending(c => c.CreatedAt)
                               .ToListAsync();
            }
        }
    }
}
