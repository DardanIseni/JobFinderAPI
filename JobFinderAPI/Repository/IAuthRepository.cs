using JwtAuthAspNet7WebAPI.Dtos;
using JwtAuthAspNet7WebAPI.Entities;
using System.Threading.Tasks;

namespace JwtAuthAspNet7WebAPI.Repository
{
    public interface IAuthRepository
    {
        Task<AuthServiceResponseDto> LoginAsync(LoginDto loginDto);
        Task<AuthServiceResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthServiceResponseDto> RegisterDemanderAsync(RegisterDto registerDto);
        Task<AuthServiceResponseDto> RegisterSeekerAsync(RegisterDto registerDto);
        Task<AuthServiceResponseDto> SeedRolesAsync();
    }
}