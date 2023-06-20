using JwtAuthAspNet7WebAPI.Dtos;
using JwtAuthAspNet7WebAPI.Entities;

namespace JwtAuthAspNet7WebAPI.Services;

public interface IWorkService
{
    Task<Work> CreateWork(WorkCreateDto workCreateDto);
    Task<Work> GetWorkById(int id);
    Task<List<Work>> GetWorks();
    Task<List<Work>> GetWorksByDemanderId();
    // Task<Work> UpdateWork(int id, WorkUpdateDto workUpdateDto);
    Task<bool> DeleteWork(int id);
}