using System.ComponentModel.DataAnnotations;

namespace _15PuzzleGame.Entities
{
    public class Result
    {
        public int Id { get; set; }

        [Required]
        public int Turns { get; set; }

        [Required]
        public string Time { get; set; }
        public DateTime? Date { get; set; }

        [Required]
        public int MyUserId { get; set; }
    }
}
