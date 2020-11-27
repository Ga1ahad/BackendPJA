using Clothesy.Domain.Entities;
using Clothesy.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Clothesy.Api.Controllers
{

    [Route("api/users/{idUser:int}/suitcases/{idSuitcase:int}")]
    [ApiController]
    public class ClothingSuitcaseController : Controller
    {
        private readonly ClothesyDbContext _context;

        public ClothingSuitcaseController(ClothesyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetClothesSuitcase(int idUser, int idSuitcase)
        {
            var clothes = from c in _context.Clothing
                          join cs in _context.ClothingSuitcase
                          on c.idClothing equals cs.idClothing
                          where cs.idSuitcase == idUser && c.idUser == idSuitcase
                          select c;

            if (clothes == null)
            {
                return NotFound();
            }

            return Ok(clothes);
        }

        [HttpPost]
        public IActionResult CreateClothingsSuitcase(ClothingSuitcase clothingSuitcase)
        {
            _context.ClothingSuitcase.Add(clothingSuitcase);
            _context.SaveChanges();
            return StatusCode(201, clothingSuitcase);
        }



    }
}