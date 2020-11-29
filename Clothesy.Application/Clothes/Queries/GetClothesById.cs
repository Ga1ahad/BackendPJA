using Clothesy.Application.Persistence;
using Clothesy.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clothesy.Application.Clothes.Queries
{
    public class GetClothesById : IRequest<Clothing>
    {
        public int idClothing { get; set; }


        public class GetClothesByIdHandler : IRequestHandler<GetClothesById, Clothing>
        {
            private readonly IClothesyDb _context;
            public GetClothesByIdHandler(IClothesyDb context)
            {
                _context = context;
            }

            public async Task<Clothing> Handle(GetClothesById request, CancellationToken cancellationToken)
            {
                return _context.Clothing.FirstOrDefault(t => t.idClothing == request.idClothing);
            }
        }
    }
}
