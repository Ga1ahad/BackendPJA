﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Clothesy.Domain.Entities;
using Clothesy.Application.Clothes.Queries.GetClothesFromSuitcase;
using Clothesy.Application.Persistence;

namespace Clothesy.Api.Controllers
{

    [Route("api/users/{idUser:int}")]
    [ApiController]
    public class ClothingController : BaseController
    {
        public ClothingController(IClothesyDb context) : base(context)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetClothings(int idUser)
        {
            var clothings = await Mediator.Send(new GetClothesFromSuitcase
            {
                IdUser = idUser
            });
            return Ok(clothings);
        }

        [HttpGet("{IdClothing:int}")]
        public IActionResult GetClothings(int idUser, int id)
        {

            var cloth = Context.Clothing.FirstOrDefault(c => c.IdClothing == id);
            if (cloth == null)
            {
                return NotFound();
            }
            return Ok(cloth);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public IActionResult CreateClothings(Clothing clothing)
        {
            Context.Clothing.Add(clothing);
            Context.SaveChanges();
            return StatusCode(201, clothing);
        }

        [HttpPut("{IdClothing:int}")]
        public IActionResult Update(int IdClothing, Clothing updatedClothing)
        {

            if (Context.Clothing.Count(c => c.IdClothing == IdClothing) == 0)
            {
                return NotFound();
            }
            Context.Clothing.Attach(updatedClothing);
            Context.Entry(updatedClothing).State = EntityState.Modified;
            Context.SaveChanges();

            return Ok(updatedClothing);
        }

        [HttpDelete("{IdClothing:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Delete(int id)
        {
            var cloth = Context.Clothing.FirstOrDefault(c => c.IdClothing == id);
            if (cloth == null)
            {
                return NotFound();
            }
            Context.Clothing.Remove(cloth);
            Context.SaveChanges();

            return Ok(cloth);
        }
    }
}