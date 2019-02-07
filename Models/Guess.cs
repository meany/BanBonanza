using System;

namespace dm.BanBonanza.Models
{
    public class Guess
    {
        public int GuessId { get; set; }
        public DateTime Date { get; set; }
        public int Number { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public ulong MessageId { get; set; }
        public GuessResponse Response { get; set; }
    }
}