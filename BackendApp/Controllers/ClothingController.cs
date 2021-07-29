using Clothesy.Application.Clothes.Queries;
using Clothesy.Application.Clothes.Commands;
using Clothesy.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Clothesy.Api.DTOs;
using Clothesy.Application.Services;
using System.Security.Claims;
using System.Collections.Generic;
using System;

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
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var req = new GetClothesFromUser { idUser = Int32.Parse(id) };
            var res = await _mediator.Send(req);
            return Ok(res);

        }

        [HttpGet("{idClothing:int}")]
        public async Task<IActionResult> GetClothings(int idUser, int idClothing)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var req = new GetClothesById { idUser = Int32.Parse(id), idClothing = idClothing };
            var res = await _mediator.Send(req);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClothings([FromForm] string name, [FromForm] int idClothingType, [FromForm] string tags, IFormFile image)
        {

            var imageResponse = await AmazonS3Service.UploadObject(image);
            var idUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            CreateClothesCommand command = new CreateClothesCommand()
            {
                Name = name,
                idUser = Int32.Parse(idUser),
                Url = imageResponse.FileName,
                idClothingType = idClothingType,
                Tags = tags
            };
            var commandResult = await _mediator.Send(command);
            return Ok(commandResult);
        }

        [HttpPut("{idClothing:int}")]
        public async Task<IActionResult> Update([FromForm] string name, [FromForm] int idClothingType, [FromForm] string tags, IFormFile image)
        {
            var idUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UpdateClothesCommand command = new UpdateClothesCommand()
            {
                Name = name,
                idUser = Int32.Parse(idUser),
                idClothingType = idClothingType,
                Tags = tags
            };
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