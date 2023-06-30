using System.ComponentModel.DataAnnotations;

namespace _15PuzzleGame.Models
{
    public class GetResultsModel : PaginationModel
    {
        [Required]
        public int UserId { get; set; }
    }
}
