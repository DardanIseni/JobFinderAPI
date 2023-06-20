using JwtAuthAspNet7WebAPI.Dtos;
using JwtAuthAspNet7WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthAspNet7WebAPI.Controllers
{
    [Route("api/work")]
    [ApiController]
    [Authorize]
    public class WorkController : ControllerBase
    {
        private readonly IWorkService _workService;

        public WorkController(IWorkService workService)
        {
            _workService = workService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateWork([FromBody] WorkCreateDto workCreateDto)
        {
            var work = await _workService.CreateWork(workCreateDto);
            return Ok(work);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkById(int id)
        {
            var work = await _workService.GetWorkById(id);
            if (work is null)
                return NotFound();

            return Ok(work);
        }

        [HttpGet]
        public async Task<IActionResult> GetWorks()
        {
            var works = await _workService.GetWorks();
            return Ok(works);
        }

        [HttpGet("getWorksByDemanderId")]
        [Authorize]
        public async Task<IActionResult> GetWorksByDemanderId()
        {
            var works = await _workService.GetWorksByDemanderId();
            return Ok(works);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteWork(int id)
        {
            var success = await _workService.DeleteWork(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
