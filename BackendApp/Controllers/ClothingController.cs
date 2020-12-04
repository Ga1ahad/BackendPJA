using Clothesy.Application.Clothes.Queries;
using Clothesy.Application.Clothes.Commands;
using Clothesy.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Clothesy.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClothingController : Controller
    {
        private readonly ClothesyDbContext _context;
        private readonly IMediator _mediator;
        public ClothingController(ClothesyDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetClothings(int idUser)
        {
            var req = new GetClothesFromUser { idUser = 1 };
            var res = await _mediator.Send(req);
            return Ok(res);

        }

        [HttpGet("{idClothing:int}")]
        public async Task<IActionResult> GetClothings(int idUser, int idClothing)
        {
            var req = new GetClothesFromUser { idUser = 1, idClothing = idClothing };
            var res = await _mediator.Send(req);
            return Ok(res);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateClothings(CreateClothesCommand command)
        {
            var commandResult = await _mediator.Send(command);
            return Ok(commandResult);
        }

        [HttpPut("{idClothing:int}")]
        public async Task<IActionResult> Update(int idClothing, UpdateClothingCommand command)
        {
            command.idClothing = idClothing;
            var commandResult = await _mediator.Send(command);
            return Ok(commandResult);
        }

        [HttpDelete("{idClothing:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int idClothing)
        {
            var req = new DeleteClothingCommand { idClothing = idClothing };
            var res = await _mediator.Send(req);
            return Ok(res);
        }
    }
}