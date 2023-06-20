using JwtAuthAspNet7WebAPI.Dtos;
using JwtAuthAspNet7WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthAspNet7WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // Route For Seeding my roles to DB
        [HttpPost]
        [Route("seed-roles")]
        public async Task<IActionResult> seedRoles()
        {
            var seedRoles = await _authService.SeedRolesAsync();
            return Ok(seedRoles);
        }

        [HttpPost]
        [Route("register/demander")]
        public async Task<IActionResult> RegisterDemander([FromBody] RegisterDto registerDto)
        {

            var registerResult = await _authService.RegisterDemanderAsync(registerDto);

            if(registerResult.IsSucceed)
                return Ok(registerResult);

            return BadRequest(registerResult);
        }
        
        [HttpPost]
        [Route("register/seeker")]
        public async Task<IActionResult> RegisterSeeker([FromBody] RegisterDto registerDto)
        {

            var registerResult = await _authService.RegisterSeekerAsync(registerDto);

            if(registerResult.IsSucceed)
                return Ok(registerResult);

            return BadRequest(registerResult);
        }


        // Route Login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
           var loginResult = await _authService.LoginAsync(loginDto);

           if(loginResult.IsSucceed)
                return Ok(loginResult);

           return Unauthorized(loginResult);
        }

    }
}


