using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dm.BanBonanza.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public ulong UserId { get; set; }
        public string Name { get; set; }
        public bool IsAlt { get; set; }

        [InverseProperty("StartedBy")]
        public List<Session> SessionsStarted { get; set; }
        [InverseProperty("Winner")]
        public List<Game> GamesWon { get; set; }
        public List<Guess> Guesses { get; set; }
    }
}
