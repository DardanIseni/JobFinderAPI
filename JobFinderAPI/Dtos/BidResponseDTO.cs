using JwtAuthAspNet7WebAPI.Entities;

namespace JwtAuthAspNet7WebAPI.Dtos;

public class BidResponseDTO
{
    public bool IsSucceed { get; set; }

    public string? Message { get; set; }
        
    public List<Bid>?  Bids { get; set; }
}