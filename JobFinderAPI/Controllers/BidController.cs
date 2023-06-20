using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtAuthAspNet7WebAPI.Dtos;
using JwtAuthAspNet7WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthAspNet7WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IBidService bidService;

        public BidController(IBidService bidService)
        {
            this.bidService = bidService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBid([FromBody] BidCreateDto bidCreateDto)
        {
            var work = await bidService.CreateBid(bidCreateDto);
            return Ok(work);
        }
        
        [HttpGet("getBidsOfSeeker")]
        [Authorize]
        public async Task<IActionResult> GetBidsOfSeeker()
        {
            var bids = await  bidService.GetBidsOfSeeker();
            return Ok(bids);
        }
        
        [HttpGet("getBidsOfWork/{workId}")]
        [Authorize]
        public async Task<IActionResult> GetBidsOfWork(int workId)
        {
            var bids = await bidService.GetBidsOfWork(workId);
            return Ok(bids);
        }
    }
}
