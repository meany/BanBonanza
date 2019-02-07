namespace dm.BanBonanza
{
    public class Config
    {
        public string BotName { get; set; }
        public string BotPrefix { get; set; }
        public string BotToken { get; set; }

        public ulong ChannelId { get; set; }
        public ulong AdminRoleId { get; set; }
        public string EmoteGood { get; set; }
        public string EmoteBad { get; set; }
    }
}