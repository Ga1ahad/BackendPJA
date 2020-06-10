using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{

    [Route("api/users/{IdUser:int}/[controller]")]
    [ApiController]
    public class SuitcaseController : Controller
    {
        private readonly s15264Context _context;

        public SuitcaseController(s15264Context context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetSuitcases(int IdUser)
        {


            var suitcases = from Suitcase in _context.Suitcase
                            where IdUser.Equals(Suitcase.IdUser)
                            select Suitcase;

            var suitcases2 = from s in _context.Suitcase
                             join t in _context.Trip
                             on s.IdTrip equals t.IdTrip
                             where IdUser.Equals(s.IdUser)
                             select new { s, t.IdTrip, t.TripName, t.StartTrip, t.EndTrip, t.Country, t.City, t.ZipCode };

            return Ok(suitcases2);
        }


        [HttpGet("{IdSuitcase:int}")]
        public IActionResult GetSuitcases(int IdUser, int id)
        {

            var suitcase = _context.Suitcase.FirstOrDefault(s => s.IdSuitcase == id);
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

        [HttpPut("{IdSuitcase:int}")]
        public IActionResult Update(int IdSuitcase, Suitcase updatedSuitcase)
        {

            if (_context.Suitcase.Count(s => s.IdSuitcase == IdSuitcase) == 0)
            {
                return NotFound();
            }
            _context.Suitcase.Attach(updatedSuitcase);
            _context.Entry(updatedSuitcase).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(updatedSuitcase);
        }

        [HttpDelete("{IdSuitcase:int}")]
        public IActionResult Delete(int id)
        {
            var suitcase = _context.Suitcase.FirstOrDefault(s => s.IdSuitcase == id);
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