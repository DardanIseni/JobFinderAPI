using JwtAuthAspNet7WebAPI.Dtos;
using JwtAuthAspNet7WebAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtAuthAspNet7WebAPI.OtherObjects;

namespace JwtAuthAspNet7WebAPI.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<AuthServiceResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);

            if (user is null)
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid Credentials"
                };

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!isPasswordCorrect)
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid Credentials"
                };

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("JWTID",Guid.NewGuid().ToString()),
                new Claim("FirstName",user.FirstName),
                new Claim("LastName",user.LastName),
                new Claim("Id",user.Id),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GenerateNewJsonWebToken(authClaims);

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = token
            };
        }

        public async Task<AuthServiceResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var isExistUser = await _userManager.FindByNameAsync(registerDto.UserName);

            if (isExistUser != null)
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "UserName already exists"
                };

            ApplicationUser newUser = new ApplicationUser()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var createUserResult = await _userManager.CreateAsync(newUser, registerDto.Password);

            if (!createUserResult.Succeeded)
            {
                var errorString = "User Creation Failed Because: ";

                foreach (var error in createUserResult.Errors)
                {
                    errorString += " # " + error.Description;
                }

                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = errorString
                };
            }

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                User = newUser
            };
        }

        public async Task<AuthServiceResponseDto> RegisterDemanderAsync(RegisterDto registerDto)
        {
            var registerResponse = await RegisterAsync(registerDto);
            if (registerResponse.IsSucceed)
            {
                var newUser = registerResponse.User;

                await _userManager.AddToRoleAsync(newUser, StaticUserRoles.JobDemander);

                return new AuthServiceResponseDto()
                {
                    IsSucceed = true,
                    Message = "User Created Successfully",
                    User = newUser
                };
            }

            return registerResponse;
        }

        public async Task<AuthServiceResponseDto> RegisterSeekerAsync(RegisterDto registerDto)
        {
            var registerResponse = await RegisterAsync(registerDto);
            if (registerResponse.IsSucceed)
            {
                var newUser = registerResponse.User;

                await _userManager.AddToRoleAsync(newUser, StaticUserRoles.JobSeeker);

                return new AuthServiceResponseDto()
                {
                    IsSucceed = true,
                    Message = "User Created Successfully",
                    User = newUser
                };
            }

            return registerResponse;
        }

        public async Task<AuthServiceResponseDto> SeedRolesAsync()
        {
            bool isOwnerRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.JobDemander);
            bool isAdminRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.JobSeeker);

            if (isOwnerRoleExists && isAdminRoleExists)
                return new AuthServiceResponseDto()
                {
                    IsSucceed = true,
                    Message = "Roles Seeding is Already Done"
                };

            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.JobDemander));
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.JobSeeker));

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "Role seeding Done Successfully"
            };
        }

        private string GenerateNewJsonWebToken(List<Claim> claims)
        {
            var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:secret"]));

            var tokenObject = new JwtSecurityToken(
                issuer: _configuration["jwt:validissuer"],
                audience: _configuration["jwt:validaudience"],
                expires: DateTime.Now.AddHours(1),
                claims: claims,
                signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
            );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);

            return token;
        }
    }
}
