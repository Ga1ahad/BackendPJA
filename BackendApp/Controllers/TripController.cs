using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clothesy.Domain.Entities;
using Clothesy.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clothesy.Api.Controllers
{

    [Route("api/users/{IdUser:int}/[controller]")]
    [ApiController]
    public class TripController : Controller
    {
        private readonly s15264Context _context;

        public TripController(s15264Context context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetTrips(int IdUser)
        {


            var trips = from t in _context.Trip
                        join s in _context.Suitcase
                        on t.IdTrip equals s.IdTrip
                        where s.IdUser == IdUser
                        select t;


            return Ok(trips.Distinct());
        }

        [HttpGet("{IdTrip:int}")]
        public IActionResult GetTrip(int IdUser, int id)
        {

            var trip = _context.Trip.FirstOrDefault(t => t.IdTrip == id);
            if (trip == null)
            {
                return NotFound();
            }
            return Ok(trip);
        }

        [HttpPost]
        public IActionResult CreateTrip(Trip trip)
        {
            _context.Trip.Add(trip);
            _context.SaveChanges();
            return StatusCode(201, trip);
        }

        [HttpPut("{IdTrip:int}")]
        public IActionResult Update(int IdTrip, Trip updatedTrip)
        {

            if (_context.Trip.Count(t => t.IdTrip == IdTrip) == 0)
            {
                return NotFound();
            }
            _context.Trip.Attach(updatedTrip);
            _context.Entry(updatedTrip).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(updatedTrip);
        }

        [HttpDelete("{IdTrip:int}")]
        public IActionResult Delete(int id)
        {
            var trip = _context.Trip.FirstOrDefault(t => t.IdTrip == id);
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