using Clothesy.Application.Persistence;
using Clothesy.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clothesy.Application.Trips.Commands
{
    public class UpdateTripCommand : IRequest<int>
    {
        public int idTrip { get; set; }
        public string TripName { get; set; }
        public DateTime StartTrip { get; set; }
        public DateTime EndTrip { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public int idUser { get; set; }

        public class CreateTripCommandHandler : IRequestHandler<UpdateTripCommand, int>
        {
            private readonly IClothesyDb _context;
            public CreateTripCommandHandler(IClothesyDb context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpdateTripCommand request, CancellationToken cancellationToken)
            {
                var trip = await _context.Trip.Where(t => t.idTrip == request.idTrip).FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (trip == null)
                {
                    return default;
                }
                else
                {
                    trip.TripName = request.TripName;
                    trip.StartTrip = request.StartTrip;
                    trip.EndTrip = request.EndTrip;
                    trip.City = request.City;
                    trip.ZipCode = request.ZipCode;
                    await _context.SaveChangesAsync(cancellationToken);
                    return trip.idTrip;
                }
            }
        }
    }
}
