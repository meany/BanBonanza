using System;
using System.Collections.Generic;

namespace dm.BanBonanza.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public DateTime Start { get; set; }
        public User StartedBy { get; set; }
        public int StartedById { get; set; }
        public int Pot { get; set; }
        public string Name { get; set; }
        public decimal Difficulty { get; set; }
        public DateTime End { get; set; }

        public List<Game> Games { get; set; }
    }
}
