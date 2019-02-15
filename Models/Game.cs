using System;
using System.Collections.Generic;

namespace dm.BanBonanza.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public DateTime Start { get; set; }
        public int Lower { get; set; }
        public int Upper { get; set; }
        public int Number { get; set; }
        public decimal Difficulty { get; set; }
        public int MaxGuesses { get; set; }
        public DateTime End { get; set; }
        public int Reward { get; set; }

        public int WinnerId { get; set; }
        public User Winner { get; set; }

        public List<Guess> Guesses { get; set; }
        public Session Session { get; set; }
    }
}