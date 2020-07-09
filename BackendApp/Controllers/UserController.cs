﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Clothesy.ApiApp.DTOs;
using Clothesy.Domain.Entities;
using Clothesy.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Clothesy.ApiApp.Controllers
{
    public class UserController : Controller
    {
        public IConfiguration Configuration { get; set; }

        private readonly s15264Context _context;

        public UserController(IConfiguration configuration, s15264Context context)
        {
            Configuration = configuration;
            _context = context;
        }

        [HttpPost]
        public IActionResult Login(LoginRequestDto request)
        {

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Name, "Jan"),
            new Claim(ClaimTypes.Role, "xyz"),
            new Claim(ClaimTypes.Role, "abc"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: "Gakko",
                audience: "Students",
                claims: claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = Guid.NewGuid()
            });
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