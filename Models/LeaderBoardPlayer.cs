﻿using System.ComponentModel.DataAnnotations;

namespace LeaderBoard.Models
{
    public class LeaderBoardPlayer
    {
        [Required]
        public string? Id { get; set; } = $"player:{Guid.NewGuid()}";

        [Required]
        public int Mmr { get; set; }
    }
}