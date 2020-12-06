using Clothesy.Persistence;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using Clothesy.Application.Trips.Commands;
using Clothesy.Application.Trips.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Clothesy.Domain.Entities;
using System.Security.Claims;
using System.Linq;
using System;

namespace Clothesy.Api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
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
            var userName = User.Identity.Name;
            var req = new GetTripsFromUser { idUser = Int16.Parse(userName) };
            var res = await _mediator.Send(req);
            return Ok(res);

        }

        [HttpGet("{idTrip:int}")]
        public async Task<IActionResult> GetTrip(int idUser, int idTrip)
        {
            //       var user = await _userManager.GetUserAsync(User);
            var req = new GetTripByIdFromUser { idUser = 11, idTrip = idTrip };
            var res = await _mediator.Send(req);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrip(CreateTripCommand command)
        {
            var userName = User.Identity.Name;
            command.idUser = Int16.Parse(userName);
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