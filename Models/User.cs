using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dm.BanBonanza.Models
{
    public class User
    {
        [Key]
        public ulong UserId { get; set; }
        public string Name { get; set; }
        public bool IsAlt { get; set; }

        public List<Session> SessionsStarted { get; set; }
        public List<Game> GamesWon { get; set; }
        public List<Guess> Guesses { get; set; }
    }
}
