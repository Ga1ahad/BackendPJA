using Clothesy.Application.Persistence;
using Clothesy.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clothesy.Application.Trips.Commands
{
    public class CreateUserCommand : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //   public DateTime CreatedAt { get; set; }
        //   public DateTime DeletedAt { get; set; }
        // public DateTime LastLogin { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
        {
            private readonly IClothesyDb _context;
            public CreateUserCommandHandler(IClothesyDb context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var user = new User();
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Email = request.Email;
                user.Password = request.Password;
                //    user.CreatedAt = request.CreatedAt;
                //     user.DeletedAt = request.DeletedAt;
                //    user.LastLogin = request.LastLogin;
                _context.User.Add(user);
                await _context.SaveChangesAsync(cancellationToken);
                return user.idUser;
            }
        }
    }
}
