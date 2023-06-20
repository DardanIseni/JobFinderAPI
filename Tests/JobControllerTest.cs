using JwtAuthAspNet7WebAPI.Controllers;
using JwtAuthAspNet7WebAPI.Dtos;
using JwtAuthAspNet7WebAPI.Entities;
using JwtAuthAspNet7WebAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests;

public class JobControllerTest
{
        private readonly WorkController _workController;
        private readonly Mock<IWorkService> _workServiceMock;

        public JobControllerTest()
        {
            _workServiceMock = new Mock<IWorkService>();
            _workController = new WorkController(_workServiceMock.Object);
        }

        [Fact]
        public async Task CreateWork_ValidWorkCreateDto_ReturnsOkResult()
        {
            var workCreateDto = new WorkCreateDto
            {
                Title = "Sample Title",
                Description = "Sample Description",
                Address = "Sample Address",
                IsActive = true,
                DemanderId = "demander123"
            };

            var expectedWork = new Work()
            {
                Title = workCreateDto.Title,
                Description = workCreateDto.Description,
                Address = workCreateDto.Address,
                IsActive = workCreateDto.IsActive,
                DemanderId = workCreateDto.DemanderId
            };
            _workServiceMock.Setup(service => service.CreateWork(workCreateDto)).ReturnsAsync(expectedWork);

            var result = await _workController.CreateWork(workCreateDto);

            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(expectedWork, okResult.Value);
        }

        [Fact]
        public async Task GetWorkById_ExistingId_ReturnsOkResult()
        {
            var id = 1; // Specify an existing work ID
            var expectedWork = new Work()
            {
                Id = 1,
                Title = "string",
                Description = "string",
                Address = "string",
                IsActive = true,
                DemanderId = "cab57439-1c87-434f-a5c5-7534aa4489ec",
            };
            _workServiceMock.Setup(service => service.GetWorkById(id)).ReturnsAsync(expectedWork);
        
            var result = await _workController.GetWorkById(id);
        
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(expectedWork, okResult.Value);
        }

        [Fact]
        public async Task GetWorkById_NonExistingId_ReturnsNotFoundResult()
        {
            var id = 255; 
            var expectedResult = new Work();
            _workServiceMock.Setup(service => service.GetWorkById(id)).ReturnsAsync(expectedResult);
        
            var result = await _workController.GetWorkById(id);
        
            Assert.IsType<NotFound>(result);
        }

        [Fact]
        public async Task GetWorks_ReturnsOkResult()
        {
            var expectedWorks = new List<Work>();
            _workServiceMock.Setup(service => service.GetWorks()).ReturnsAsync(expectedWorks);

            var result = await _workController.GetWorks();

            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(expectedWorks, okResult.Value);
        }

        [Fact]
        public async Task GetWorksByDemanderId_ReturnsOkResult()
        {
            var expectedWorks = new List<Work>();
            _workServiceMock.Setup(service => service.GetWorksByDemanderId()).ReturnsAsync(expectedWorks);

            var result = await _workController.GetWorksByDemanderId();

            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(expectedWorks, okResult.Value);
        }

        [Fact]
        public async Task DeleteWork_ExistingId_ReturnsNoContentResult()
        {
            var id = 1;
            var success = true; 
            _workServiceMock.Setup(service => service.DeleteWork(id)).ReturnsAsync(success);

            var result = await _workController.DeleteWork(id);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteWork_NonExistingId_ReturnsNotFoundResult()
        {
            var id = 1; 
            var success = false; 
            _workServiceMock.Setup(service => service.DeleteWork(id)).ReturnsAsync(success);

            var result = await _workController.DeleteWork(id);

            Assert.IsType<NotFoundResult>(result);
        }
}