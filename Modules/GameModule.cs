using Discord;
using Discord.Commands;
using dm.BanBonanza.Data;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace dm.BanBonanza.Modules
{
    public class GameModule : ModuleBase
    {
        private readonly Config config;
        private readonly AppDbContext db;

        public GameModule(IOptions<Config> config, AppDbContext db)
        {
            this.config = config.Value;
            this.db = db;
        }

        [Command("start")]
        [Summary("Starts a new session")]
        [Remarks("If you are a game administrator, you can start a new session with this command.\n" +
            "Sessions will run until the pot is empty or until you use the **stop** command.\n")]
        public async Task Start(int pot, string name = "BanBonanza", decimal difficulty = 0)
        {
            var user = await UserUtils.GetOrUpdateUser(db, config, Context.User.Id, Context.User.ToString()).ConfigureAwait(false);
            var output = new EmbedBuilder();

            await Discord.SendAsync(Context, config.ChannelId, output).ConfigureAwait(false);
        }
    }
}