using _15PuzzleGame.Entities;
using _15PuzzleGame.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _15PuzzleGame.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ResultsController : ControllerBase
    {

        private readonly PuzzleContext _context;

        public ResultsController(PuzzleContext context)
        {
            _context = context;
        }

        [HttpPost("GetUserResults")]
        public async Task<ActionResult<ResultResponseWithCount>> GetUserResultsAsync(GetResultsModel getResultsModel)
        {
            var list = _context.Results
                .Where(r => r.MyUserId == getResultsModel.UserId)
                .Select(r => new ResultResponse
                {
                    Turns = r.Turns,
                    Time = r.Time,
                    Date = r.Date
                });
            var resultResponseWithCount = new ResultResponseWithCount
            {
                Results = await list
                .Skip(getResultsModel.ItemsPerPage * (getResultsModel.PageNumber - 1))
                .Take(getResultsModel.ItemsPerPage)
                .ToListAsync(),
                TotalCount = await list.CountAsync()
            };
            return Ok(resultResponseWithCount);
        }


        [HttpPost("AddResult")]
        public async Task<ActionResult> AddResult(Result newResult)
        {
            newResult.Date = DateTime.UtcNow;
            await _context.Results.AddAsync(newResult);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}