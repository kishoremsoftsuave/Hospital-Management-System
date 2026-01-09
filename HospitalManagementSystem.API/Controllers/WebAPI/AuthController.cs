using HospitalManagementSystem.Application.Services;
using HospitalManagementSystem.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HospitalManagementSystem.API.Controllers.WebAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "admin123")
            {
                return Ok(_tokenService.CreateToken(1, Roles.Admin));
            }

            if (username == "doctor" && password == "doctor123")
            {
                return Ok(_tokenService.CreateToken(2, Roles.Doctor));
            }

            if (username == "reception" && password == "rec123")    
            {
                return Ok(_tokenService.CreateToken(3, Roles.Receptionist));
            }

            if (username == "patient" && password == "pat123")
            {
                return Ok(_tokenService.CreateToken(4, Roles.Patient));
            }

            return Unauthorized("Invalid credentials");
        }
    }
}
