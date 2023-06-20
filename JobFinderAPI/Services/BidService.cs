using JwtAuthAspNet7WebAPI.Dtos;
using JwtAuthAspNet7WebAPI.Entities;
using JwtAuthAspNet7WebAPI.Repository;

namespace JwtAuthAspNet7WebAPI.Services;

public class BidService: IBidService
{
    
    private readonly IBidRepository iBidRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BidService(IBidRepository iBidRepository, IHttpContextAccessor _httpContextAccessor)
    {
        this.iBidRepository = iBidRepository;
        this._httpContextAccessor = _httpContextAccessor;
    }
    
    public Task<List<Bid>> GetBidsOfSeeker()
    {
        var seekerId = _httpContextAccessor.HttpContext.User.Claims.First(claim => claim.Type == "Id");
        var bids = iBidRepository.GetBidsOfSeeker(seekerId.Value);
        return bids;
    }

    public Task<List<Bid>> GetBidsOfWork(int workId)
    {
        var bids = iBidRepository.GetBidsOfWork(workId);
        return bids;
    }

    public Task<Bid> CreateBid(BidCreateDto bidCreateDto)
    {
        return iBidRepository.CreateBid(bidCreateDto);
    }
}