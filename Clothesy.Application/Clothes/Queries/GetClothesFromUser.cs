using Clothesy.Application.Persistence;
using Clothesy.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clothesy.Application.Clothes.Queries
{
    public class GetClothesFromUser : IRequest<Clothing>
    {
        public int idClothing { get; set; }
        public int idUser { get; set; }


        public class GetClothesFromUserHandler : IRequestHandler<GetClothesFromUser, Clothing>
        {
            private readonly IClothesyDb _context;
            public GetClothesFromUserHandler(IClothesyDb context)
            {
                _context = context;
            }

            public async Task<Clothing> Handle(GetClothesFromUser request, CancellationToken cancellationToken)
            {
                return _context.Clothing.FirstOrDefault(t => t.idClothing == request.idClothing);
            }
        }
    }
}
