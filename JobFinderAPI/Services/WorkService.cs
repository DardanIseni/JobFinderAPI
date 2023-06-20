using System.Security.Claims;
using JwtAuthAspNet7WebAPI.Dtos;
using JwtAuthAspNet7WebAPI.Entities;
using JwtAuthAspNet7WebAPI.Repository;

namespace JwtAuthAspNet7WebAPI.Services;

public class WorkService : IWorkService
{
    private readonly IWorkRepository _workRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public WorkService(IWorkRepository workRepository, IHttpContextAccessor _httpContextAccessor)
    {
        _workRepository = workRepository;
        this._httpContextAccessor = _httpContextAccessor;
    }

    public Task<Work> CreateWork(WorkCreateDto workCreateDto)
    {
        return _workRepository.CreateWork(workCreateDto);
    }

    public Task<Work> GetWorkById(int id)
    {
        return _workRepository.GetWorkById(id);
    }

    public Task<List<Work>> GetWorks()
    {
        return _workRepository.GetWorks();
    }

    public Task<List<Work>> GetWorksByDemanderId()
    {
        var demanderId = _httpContextAccessor.HttpContext.User.Claims.First(claim => claim.Type == "Id");
        return _workRepository.GetWorksByDemanderId(demanderId.Value);
    }

    public async Task<bool> DeleteWork(int id)
    {
        var work = await _workRepository.GetWorkById(id);

        if (work == null)
            return false;
        var userIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;

        // Check if the user trying to delete is the creator of the work
        if (work.DemanderId != userIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            return false;

        return await _workRepository.DeleteWork(id);
    }
}
