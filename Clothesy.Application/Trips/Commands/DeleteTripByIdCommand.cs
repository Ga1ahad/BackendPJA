using Clothesy.Application.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clothesy.Application.Trips.Commands
{
    public class DeleteTripByIdCommand : IRequest<int>
    {
        public int idTrip { get; set; }

        public class DeleteTripHandler : IRequestHandler<DeleteTripByIdCommand, int>
        {
            private readonly IClothesyDb _context;
            public DeleteTripHandler(IClothesyDb context)
            {
                _context = context;
            }

            public async Task<int> Handle(DeleteTripByIdCommand request, CancellationToken cancellationToken)
            {
                var trip = await _context.Trip.Where(t => t.idTrip == request.idTrip).FirstOrDefaultAsync();
                if (trip == null) return default;
                _context.Trip.Remove(trip);
                await _context.SaveChangesAsync(cancellationToken);
                return trip.idTrip;

            }
        }
    }
}
