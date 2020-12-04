using MediatR;
using Clothesy.ApiApp.DTOs;
using Clothesy.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Clothesy.Application.Trips.Commands;
using System.Linq;


namespace Clothesy.ApiApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ClothesyDbContext _context;
        private readonly IMediator _mediator;
        private IConfiguration _configuration;


        public UserController(IConfiguration configuration, ClothesyDbContext context, IMediator mediator)
        {
            _configuration = configuration;
            _context = context;
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            request.Email = request.Email.ToLower();
            var user = _context.User.SingleOrDefault(x => x.Email == request.Email);
            if (user == null)
                return Unauthorized();

            if (!(BCrypt.Net.BCrypt.Verify(request.Password, user.Password)))
                return Unauthorized();

            var claims = new[]
            {
              new Claim(JwtRegisteredClaimNames.UniqueName, request.Email),
              new Claim(ClaimTypes.NameIdentifier, user.idUser.ToString()),
              new Claim(ClaimTypes.Name, user.Email),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("condimentumvestibulumSuspendissesitametpulvinarorcicondimentummollisjusto"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
            (
                issuer: _configuration["AuthSection:JWtConfig:Issuer"],
                audience: _configuration["AuthSection:JWtConfig:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = Guid.NewGuid()
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserCommand command)
        {
            command.Password = BCrypt.Net.BCrypt.HashPassword(command.Password);
            var commandResult = await _mediator.Send(command);
            return Ok(commandResult);
        }

    }
}