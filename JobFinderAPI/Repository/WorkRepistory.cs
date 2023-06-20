using JwtAuthAspNet7WebAPI.DbContext;
using JwtAuthAspNet7WebAPI.Dtos;
using JwtAuthAspNet7WebAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthAspNet7WebAPI.Repository;

public class WorkRepository : IWorkRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public WorkRepository(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task<Work> CreateWork(WorkCreateDto workCreateDto)
    {
        var user = await _userManager.FindByIdAsync(workCreateDto.DemanderId);

        var newWork = new Work()
        {
            Address = workCreateDto.Address,
            Demander = user,
            Description = workCreateDto.Description,
            Title = workCreateDto.Title,
            DemanderId = user.Id
        };

        _dbContext.Works.Add(newWork);
        await _dbContext.SaveChangesAsync();

        return newWork;
    }

    public async Task<Work> GetWorkById(int id)
    {
        var work = await _dbContext.Works.FindAsync(id);
        return work;
    }

    public async Task<List<Work>> GetWorks()
    {
        var works = await _dbContext.Works.ToListAsync();
        return works;
    }

    public async Task<List<Work>> GetWorksByDemanderId(string demanderId)
    {
        var works = await _dbContext.Works.Where(w => w.DemanderId == demanderId).ToListAsync();
        return works;
    }

    public async Task<bool> DeleteWork(int id)
    {
        var work = await _dbContext.Works.FindAsync(id);

        if (work == null)
            return false;

        _dbContext.Works.Remove(work);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}
