using JwtAuthAspNet7WebAPI.Dtos;
using JwtAuthAspNet7WebAPI.Entities;

namespace JwtAuthAspNet7WebAPI.Repository;

public interface IWorkRepository
{
    Task<Work> CreateWork(WorkCreateDto workCreateDto);
    Task<Work> GetWorkById(int id);
    Task<List<Work>> GetWorks();
    Task<List<Work>> GetWorksByDemanderId(string demanderId);
    Task<bool> DeleteWork(int id);
}
