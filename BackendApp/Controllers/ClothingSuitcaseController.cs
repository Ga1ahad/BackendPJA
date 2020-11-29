using Clothesy.Application.Clothes.Queries;
using Clothesy.Application.Clothes.Commands;
using Clothesy.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace Clothesy.Api.Controllers
{

    [Route("api/users/{idUser:int}/suitcases/{idSuitcase:int}")]
    [ApiController]
    public class ClothingSuitcaseController : Controller
    {
        private readonly ClothesyDbContext _context;
        private readonly IMediator _mediator;

        public ClothingSuitcaseController(ClothesyDbContext context, IMediator mediator)
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

    }
}