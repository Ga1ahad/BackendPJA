using Clothesy.Application.Persistence;
using Clothesy.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clothesy.Application.Trips.Queries
{
    public class GetTripsFromUser : IRequest<IEnumerable<Trip>>
    {
        public int idUser { get; set; }

        public class Handler : IRequestHandler<GetTripsFromUser, IEnumerable<Trip>>
        {
            private readonly IClothesyDb _context;
            public Handler(IClothesyDb context, IMediator mediator)
            {
                _context = context;
            }

            public async Task<IEnumerable<Trip>> Handle(GetTripsFromUser request, CancellationToken cancellationToken)
            {

                return await _context.Trip
                               .Where(c => c.idUser == request.idUser)
                               .ToListAsync(cancellationToken: cancellationToken);
            }
        }
    }
}
