using AutoMapper;
using BookTrackerApi.Data;
using BookTrackerApi.DTOs;
using BookTrackerApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookTrackerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UsersController(AppDbContext db, IMapper mapper, ITokenService tokenService)
        {
            _db = db;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var exists = await _db.Users.AnyAsync(u => u.Username == dto.Username);
            if (exists) return BadRequest("User exists");

            var user = _mapper.Map<User>(dto);
            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return Ok("Registered");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthDto dto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized();

            var token = _tokenService.GenerateToken(user);

            return Ok(new { token });
        }
    }

}
