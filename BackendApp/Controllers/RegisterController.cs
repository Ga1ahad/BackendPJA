using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clothesy.ApiApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clothesy.ApiApp.Controllers
{
    [Route("api/users/{IdUser:int}/[controller]")]
    [ApiController]
    public class RegisterController : Controller
    {
        private readonly s15264Context _context;
        public RegisterController(s15264Context context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
            return StatusCode(201, user);
        }

    }
}