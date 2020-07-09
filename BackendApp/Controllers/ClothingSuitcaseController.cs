using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clothesy.Domain.Entities;
using Clothesy.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Clothesy.Api.Controllers
{

    [Route("api/users/{IdUser:int}/suitcases/{IdSuitcase:int}")]
    [ApiController]
    public class ClothingSuitcaseController : Controller
    {
        private readonly s15264Context _context;

        public ClothingSuitcaseController(s15264Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetClothesSuitcase(int idUser, int idSuitcase)
        {
            var clothes = from c in _context.Clothing
                          join cs in _context.ClothingSuitcase
                          on c.IdClothing equals cs.IdClothing
                          where cs.IdSuitcase == idUser && c.IdUser == idSuitcase
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