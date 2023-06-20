using JwtAuthAspNet7WebAPI.Dtos;
using JwtAuthAspNet7WebAPI.Repository;

namespace JwtAuthAspNet7WebAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<AuthServiceResponseDto> LoginAsync(LoginDto loginDto)
        {
            return await _authRepository.LoginAsync(loginDto);
        }

        public async Task<AuthServiceResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            return await _authRepository.RegisterAsync(registerDto);
        }

        public async Task<AuthServiceResponseDto> RegisterDemanderAsync(RegisterDto registerDto)
        {
            return await _authRepository.RegisterDemanderAsync(registerDto);
        }

        public async Task<AuthServiceResponseDto> RegisterSeekerAsync(RegisterDto registerDto)
        {
            return await _authRepository.RegisterSeekerAsync(registerDto);
        }

        public async Task<AuthServiceResponseDto> SeedRolesAsync()
        {
            return await _authRepository.SeedRolesAsync();
        }
    }
}