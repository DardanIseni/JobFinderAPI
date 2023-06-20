using JwtAuthAspNet7WebAPI.Entities;

namespace JwtAuthAspNet7WebAPI.Dtos;

public class BidCreateDto
{
    public double Amount { get; set; }
    
    public int WorkId { get; set; }
    public Work Work { get; set; } = null!;
    
    public string? SeekerId { get; set; }
    public ApplicationUser Seeker { get; set; } = null!;
}