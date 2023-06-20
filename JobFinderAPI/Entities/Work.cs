using System.ComponentModel.DataAnnotations;

namespace JwtAuthAspNet7WebAPI.Entities;

public class Work
{
    [Key]
    public int Id { get; set; }
    
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Address { get; set; }

    public bool IsActive { get; set; } = true;
    
    public string? DemanderId { get; set; }
    public ApplicationUser Demander { get; set; } = null!;
    
}