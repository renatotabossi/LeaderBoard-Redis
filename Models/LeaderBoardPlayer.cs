using System.ComponentModel.DataAnnotations;

namespace LeaderBoard.Models
{
    public class LeaderBoardPlayer
    {
        [Required]
        public string? Id { get; set; }

        [Required] 
        public string? Name { get; set; }

        [Required]
        public int Mmr { get; set; }
    }
}
