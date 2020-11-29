using Clothesy.Application.Persistence;
using Clothesy.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clothesy.Application.Trips.Queries
{
    public class GetTripByIdFromUser : IRequest<Trip>
    {
        public int idTrip { get; set; }
        public int idUser { get; set; }


        public class Handler : IRequestHandler<GetTripByIdFromUser, Trip>
        {
            private readonly IClothesyDb _context;
            public Handler(IClothesyDb context)
            {
                _context = context;
            }

            public async Task<Trip> Handle(GetTripByIdFromUser request, CancellationToken cancellationToken)
            {
                return _context.Trip.FirstOrDefault(t => t.idTrip == request.idTrip);
            }
        }
    }
}
