using Clothesy.Application.Persistence;
using Clothesy.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Clothesy.Application.Tag.Queries
{
    public class GetTag : IRequest<IEnumerable<Tags>>
    {

        public class Handler : IRequestHandler<GetTag, IEnumerable<Tags>>
        {
            private readonly IClothesyDb _context;
            public Handler(IClothesyDb context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Tags>> Handle(GetTag request, CancellationToken cancellationToken)
            {
                return await _context.Tags.ToListAsync(cancellationToken: cancellationToken); ;
            }
        }
    }
}
