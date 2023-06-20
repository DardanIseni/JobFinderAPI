using JwtAuthAspNet7WebAPI.Entities;

namespace JwtAuthAspNet7WebAPI.Dtos
{
    public class AuthServiceResponseDto
    {
        public bool IsSucceed { get; set; }

        public string Message { get; set; }
        
        public ApplicationUser User { get; set; }
    }
}
