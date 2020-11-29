using Clothesy.Application.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clothesy.Application.Clothes.Commands
{
    public class DeleteClothingCommand : IRequest<int>
    {
        public int idClothing { get; set; }

        public class DeleteClothingCommandHandler : IRequestHandler<DeleteClothingCommand, int>
        {
            private readonly IClothesyDb _context;
            public DeleteClothingCommandHandler(IClothesyDb context)
            {
                _context = context;
            }

            public async Task<int> Handle(DeleteClothingCommand request, CancellationToken cancellationToken)
            {
                var clothing = await _context.Clothing.Where(t => t.idClothing == request.idClothing).FirstOrDefaultAsync();
                if (clothing == null) return default;
                _context.Clothing.Remove(clothing);
                await _context.SaveChangesAsync(cancellationToken);
                return clothing.idClothing;

            }
        }
    }
}
