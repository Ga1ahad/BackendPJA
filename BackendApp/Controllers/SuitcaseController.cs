﻿using Clothesy.Application.Clothes.Queries.GetClothesFromSuitcase;
using Clothesy.Domain.Entities;
using Clothesy.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clothesy.Api.Controllers
{

    [Route("api/users/{idUser:int}/[controller]")]
    [ApiController]
    public class SuitcaseController : Controller
    {
        private readonly ClothesyDbContext _context;

        public SuitcaseController(ClothesyDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetSuitcases(int idUser)
        {
            // var req = new GetClothesFromSuitcase { idUser = idUser };
            // var res = await Mediator.Send(req, new CancellationToken());
            //    var suitcases = from Suitcase in _context.Suitcase
            //                where IdUser.Equals(Suitcase.IdUser)
            //                  select Suitcase;

            //   var suitcases2 = from s in _context.Suitcase
            //                  join t in _context.Trip
            //                   on s.IdTrip equals t.IdTrip
            //                where IdUser.Equals(s.IdUser)
            //                select new { s, t.IdTrip, t.TripName, t.StartTrip, t.EndTrip, t.Country, t.City, t.ZipCode };

            return Ok(null);
        }


        [HttpGet("{idSuitcase:int}")]
        public IActionResult GetSuitcases(int idUser, int id)
        {

            var suitcase = _context.Suitcase.FirstOrDefault(s => s.idSuitcase == id);
            if (suitcase == null)
            {
                return NotFound();
            }
            return Ok(suitcase);
        }





        [HttpPost]
        public IActionResult CreateCuitcase(Suitcase suitcase)
        {
            _context.Suitcase.Add(suitcase);
            _context.SaveChanges();
            return StatusCode(201, suitcase);
        }

        [HttpPut("{idSuitcase:int}")]
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
        }

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