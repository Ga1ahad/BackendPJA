using Clothesy.Application.Persistence;
using Clothesy.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clothesy.Application.Trips.Commands
{
    public class CreateTripCommand : IRequest<int>
    {
        public string TripName { get; set; }
        public DateTime StartTrip { get; set; }
        public DateTime EndTrip { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public int idUser { get; set; }

        public class CreateTripCommandHandler : IRequestHandler<CreateTripCommand, int>
        {
            private readonly IClothesyDb _context;
            public CreateTripCommandHandler(IClothesyDb context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateTripCommand request, CancellationToken cancellationToken)
            {
                var trip = new Trip();
                trip.TripName = request.TripName;
                trip.StartTrip = request.StartTrip;
                trip.EndTrip = request.EndTrip;
                trip.Country = request.Country;
                trip.City = request.City;
                trip.ZipCode = request.ZipCode;
                trip.idUser = request.idUser;
                _context.Trip.Add(trip);
                await _context.SaveChangesAsync(cancellationToken);
                return trip.idTrip;
            }
        }
    }
}
