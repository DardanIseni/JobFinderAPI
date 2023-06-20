using JwtAuthAspNet7WebAPI.Dtos;

namespace JwtAuthAspNet7WebAPI.Services
{
    public interface IAuthService
    {
        Task<AuthServiceResponseDto> SeedRolesAsync();
        Task<AuthServiceResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthServiceResponseDto> RegisterDemanderAsync(RegisterDto registerDto);
        Task<AuthServiceResponseDto> RegisterSeekerAsync(RegisterDto registerDto);
        Task<AuthServiceResponseDto> LoginAsync(LoginDto loginDto);

    }
}
