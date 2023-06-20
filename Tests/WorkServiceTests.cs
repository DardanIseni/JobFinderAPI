namespace Tests;
using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using JwtAuthAspNet7WebAPI.Dtos;
using JwtAuthAspNet7WebAPI.Entities;
using JwtAuthAspNet7WebAPI.Repository;
using JwtAuthAspNet7WebAPI.Services;

public class WorkServiceTests
{
    private readonly WorkService _workService;
    private readonly Mock<IWorkRepository> _workRepositoryMock;
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;

    public WorkServiceTests()
    {
        _workRepositoryMock = new Mock<IWorkRepository>();
        _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        _workService = new WorkService(_workRepositoryMock.Object, _httpContextAccessorMock.Object);
    }

    [Fact]
    public async Task CreateWork_ValidWorkCreateDto_ReturnsWork()
    {
        var workCreateDto = new WorkCreateDto
        {
            Title = "string",
            Description = "string",
            Address = "string",
            IsActive = true,
            DemanderId = "cab57439-1c87-434f-a5c5-7534aa4489ec",
        };

        var expectedWork = new Work
        {
            Id = 1,
            Title = "string",
            Description = "string",
            Address = "string",
            IsActive = true,
            DemanderId = "cab57439-1c87-434f-a5c5-7534aa4489ec",
        };

        _workRepositoryMock.Setup(repository => repository.CreateWork(workCreateDto)).ReturnsAsync(expectedWork);

        var result = await _workService.CreateWork(workCreateDto);

        Assert.Equal(expectedWork, result);
    }

    [Fact]
    public async Task GetWorkById_ExistingId_ReturnsWork()
    {
        var id = 1; 
        var expectedWork = new Work
        {
            Id = 1,
            Title = "string",
            Description = "string",
            Address = "string",
            IsActive = true,
            DemanderId = "cab57439-1c87-434f-a5c5-7534aa4489ec",
        };

        _workRepositoryMock.Setup(repository => repository.GetWorkById(id)).ReturnsAsync(expectedWork);

        var result = await _workService.GetWorkById(id);

        Assert.Equal(expectedWork, result);
    }

    [Fact]
    public async Task GetWorkById_NonExistingId_ReturnsNull()
    {
        var id = 255;
        _workRepositoryMock.Setup(repository => repository.GetWorkById(id)).ReturnsAsync((Work)null);

        var result = await _workService.GetWorkById(id);

        Assert.Null(result);
    }
}
