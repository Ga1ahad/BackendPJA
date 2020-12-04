using Clothesy.Persistence;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using Clothesy.Application.Trips.Commands;
using Clothesy.Application.Trips.Queries;
using Microsoft.AspNetCore.Authorization;


namespace Clothesy.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : Controller
    {
        private readonly ClothesyDbContext _context;
        private readonly IMediator _mediator;

        public TripController(ClothesyDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips(int idUser)
        {
            var req = new GetTripsFromUser { idUser = 1 };
            var res = await _mediator.Send(req);
            return Ok(res);

        }

        [HttpGet("{idTrip:int}")]
        public async Task<IActionResult> GetTrip(int idUser, int idTrip)
        {
            var req = new GetTripByIdFromUser { idUser = 1, idTrip = idTrip };
            var res = await _mediator.Send(req);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrip(CreateTripCommand command)
        {
            var commandResult = await _mediator.Send(command);
            return Ok(commandResult);
        }

        [HttpPut("{idTrip:int}")]
        public async Task<IActionResult> Update(int idTrip, UpdateTripCommand command)
        {
            command.idTrip = idTrip;
            var commandResult = await _mediator.Send(command);
            return Ok(commandResult);
        }

        [HttpDelete("{idTrip:int}")]
        public async Task<IActionResult> Delete(int idUser, int idTrip)
        {
            var req = new DeleteTripByIdCommand { idTrip = idTrip };
            var res = await _mediator.Send(req);
            return Ok(res);
        }


    }
}