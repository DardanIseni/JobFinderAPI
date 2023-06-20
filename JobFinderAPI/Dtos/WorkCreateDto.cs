namespace JwtAuthAspNet7WebAPI.Dtos;

public class WorkCreateDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; }
    public string DemanderId { get; set; }
}