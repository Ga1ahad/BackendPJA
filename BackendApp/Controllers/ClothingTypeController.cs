using Clothesy.Application.Tag.Queries;
using Clothesy.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clothesy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClothingTypeController : Controller
    {
        private readonly ClothesyDbContext _context;
        private readonly IMediator _mediator;
        public ClothingTypeController(ClothesyDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;

        }
        [HttpGet]
        public async Task<IActionResult> GetClothingTypes(int idUser)
        {
            var req = new GetClothingType();
            var res = await _mediator.Send(req);
            return Ok(res);

        }
    }
}
