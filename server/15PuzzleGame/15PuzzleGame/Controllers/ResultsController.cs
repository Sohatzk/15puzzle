using _15PuzzleGame.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _15PuzzleGame.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultsController : ControllerBase
    {

        private readonly PuzzleContext _context;

        public ResultsController(PuzzleContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetUserResults")]
        public async Task<IEnumerable<Result>> GetUserResultsAsync(int userId)
        {
            return await _context.Results
                .Where(r => r.MyUserId == userId)
                .ToListAsync();
        }
    }
}