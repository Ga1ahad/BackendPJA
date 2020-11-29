using Clothesy.Application.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clothesy.Application.Clothes.Commands
{
    public class UpdateClothingCommand : IRequest<int>
    {
        public int idClothing { get; set; }

        public string Name { get; set; }

        public class UpdateClothingCommandHandler : IRequestHandler<UpdateClothingCommand, int>
        {
            private readonly IClothesyDb _context;
            public UpdateClothingCommandHandler(IClothesyDb context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpdateClothingCommand request, CancellationToken cancellationToken)
            {
                var cloting = await _context.Clothing.Where(t => t.idClothing == request.idClothing).FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (cloting == null)
                {
                    return default;
                }
                else
                {
                    cloting.Name = request.Name;
                    await _context.SaveChangesAsync(cancellationToken);
                    return cloting.idClothing;
                }
            }
        }
    }
}
