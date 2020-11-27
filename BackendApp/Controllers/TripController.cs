using Clothesy.Domain.Entities;
using Clothesy.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Clothesy.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TripController : Controller
    {
        private readonly ClothesyDbContext _context;

        public TripController(ClothesyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetTrips(int idUser)
        {
            var trips = from t in _context.Trip
                        where t.idUser == 1
                        select t;


            return Ok(trips.Distinct());
        }

        [HttpGet("{idTrip:int}")]
        public IActionResult GetTrip(int idUser, int id)
        {

            var trip = _context.Trip.FirstOrDefault(t => t.idTrip == id);
            if (trip == null)
            {
                return NotFound();
            }
            return Ok(trip);
        }

        [HttpPost]
        public IActionResult CreateTrip(Trip trip)
        {
            trip.idUser = 1;
            _context.Trip.Add(trip);
            _context.SaveChanges();
            return StatusCode(201, trip);
        }

        [HttpPut("{idTrip:int}")]
        public IActionResult Update(int idTrip, Trip updatedTrip)
        {

            if (_context.Trip.Count(t => t.idTrip == idTrip) == 0)
            {
                return NotFound();
            }
            _context.Trip.Attach(updatedTrip);
            _context.Entry(updatedTrip).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(updatedTrip);
        }

        [HttpDelete("{idTrip:int}")]
        public IActionResult Delete(int id)
        {
            var trip = _context.Trip.FirstOrDefault(t => t.idTrip == id);
            if (trip == null)
            {
                return NotFound();
            }
            _context.Trip.Remove(trip);
            _context.SaveChanges();

            return Ok(trip);
        }


    }
}