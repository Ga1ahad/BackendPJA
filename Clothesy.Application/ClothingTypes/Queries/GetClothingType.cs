using Clothesy.Application.Persistence;
using Clothesy.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Clothesy.Application.Tag.Queries
{
    public class GetClothingType : IRequest<IEnumerable<ClothingType>>
    {

        public class Handler : IRequestHandler<GetClothingType, IEnumerable<ClothingType>>
        {
            private readonly IClothesyDb _context;
            public Handler(IClothesyDb context)
            {
                _context = context;
            }

            public async Task<IEnumerable<ClothingType>> Handle(GetClothingType request, CancellationToken cancellationToken)
            {
                return await _context.ClothingType.ToListAsync(cancellationToken: cancellationToken);
            }
        }
    }
}
