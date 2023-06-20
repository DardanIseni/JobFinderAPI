using JwtAuthAspNet7WebAPI.DbContext;
using JwtAuthAspNet7WebAPI.Dtos;
using JwtAuthAspNet7WebAPI.Entities;
using Microsoft.AspNetCore.Identity;

namespace JwtAuthAspNet7WebAPI.Repository;

public class BidRepository: IBidRepository
{
    
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public BidRepository(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }
    
    
    public  async  Task<Bid> CreateBid(BidCreateDto bidCreateDto)
    {
        var user = await _userManager.FindByIdAsync(bidCreateDto.SeekerId);
        var work = _dbContext.Works.FindAsync(bidCreateDto.WorkId).Result;

        var bid = new Bid()
        {
            SeekerId = user.Id,
            Amount = bidCreateDto.Amount,
            Work = work,
            Seeker = user,
            WorkId = work.Id
        };

        await _dbContext.Bids.AddAsync(bid);
        await _dbContext.SaveChangesAsync();

        return bid;

    }

    public async Task<List<Bid>> GetBidsOfSeeker(string seekerId)
    {
        var bids = _dbContext.Bids.Where(bid => bid.SeekerId == seekerId).ToList();
        return bids;
    }

    public async Task<List<Bid>> GetBidsOfWork(int workId)
    {
        var bids = _dbContext.Bids.Where(bid => bid.WorkId == workId).ToList();
        return bids;
    }
}