using Clothesy.Domain.Entities;
using Clothesy.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Clothesy.Application.Clothes.Commands;
using System.Threading.Tasks;
using Clothesy.Application.Suitcases.Commands;
using System.Text.Json;
using System.Collections.Generic;
using System.Security.Claims;

namespace Clothesy.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SuitcaseController : Controller
    {
        private readonly ClothesyDbContext _context;
        private readonly IMediator _mediator;

        public SuitcaseController(ClothesyDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        [HttpGet]
        //    public async Task<IActionResult> GetSuitcases(int idUser)
        //  {
        //  var req = new GetClothesFromSuitcase { idUser = idUser };
        //     var res = await _mediator.Send(req);

        //    return Ok(res);
        // }

        [HttpGet("{idSuitcase:int}")]
        public async Task<IActionResult> GetSuitcases(int idSuitcase)
        {
            var req = new GetClothesFromSuitcase { idTrip = idSuitcase };
            var res = await _mediator.Send(req);
            return Ok(res);
        }

        [HttpPost("{idTrip:int}")]
        public async Task<IActionResult> CreateCuitcase(int idTrip)
        {
            //var image = clh.Image;

            //     var imageResponse = await AmazonS3Service.UploadObject(image);

            var command = new CreateSuitcaseCommand { idTrip = idTrip, };
            var userName = User.Identity.Name;
            var commandResult = await _mediator.Send(command);
            return Ok(commandResult);
        }

        /*[HttpPut("{idSuitcase:int}")]
        public IActionResult Update(int idSuitcase, Suitcase updatedSuitcase)
        {

            if (_context.Suitcase.Count(s => s.idSuitcase == idSuitcase) == 0)
            {
                return NotFound();
            }
            _context.Suitcase.Attach(updatedSuitcase);
            _context.Entry(updatedSuitcase).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(updatedSuitcase);
        }*/

        [HttpDelete("{idSuitcase:int}")]
        public IActionResult Delete(int id)
        {
            var suitcase = _context.Suitcase.FirstOrDefault(s => s.idSuitcase == id);
            if (suitcase == null)
            {
                return NotFound();
            }
            _context.Suitcase.Remove(suitcase);
            _context.SaveChanges();

            return Ok(suitcase);
        }
    }
}