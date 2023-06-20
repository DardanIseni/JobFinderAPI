using JwtAuthAspNet7WebAPI.Dtos;
using JwtAuthAspNet7WebAPI.Entities;

namespace JwtAuthAspNet7WebAPI.Repository;

public interface IBidRepository
{
    Task<Bid> CreateBid(BidCreateDto bidCreateDto);

    Task<List<Bid>> GetBidsOfSeeker(string seekerId);

    Task<List<Bid>> GetBidsOfWork(int workId);
}