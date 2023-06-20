using JwtAuthAspNet7WebAPI.Dtos;
using JwtAuthAspNet7WebAPI.Entities;

namespace JwtAuthAspNet7WebAPI.Services;

public interface IBidService
{
    Task<List<Bid>> GetBidsOfSeeker();
    Task<List<Bid>> GetBidsOfWork(int workId);

    Task<Bid> CreateBid(BidCreateDto bidCreateDto);
}