using Discord;
using Discord.Commands;
using dm.BanBonanza.Data;
using Microsoft.Extensions.Options;
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
        [RequireUserPermission(ChannelPermission.ManageMessages)]
        public async Task Start(int pot, string name = "BanBonanza", decimal difficulty = 0)
        {
            var dbUser = await UserUtils.GetOrUpdateUser(db, config, Context.User.Id, Context.User.ToString()).ConfigureAwait(false);
            var output = new EmbedBuilder();

            await Discord.SendAsync(Context, config.ChannelId, output).ConfigureAwait(false);
        }

        [Command("stop")]
        [Summary("Stops current session")]
        [Remarks("If you are a game administrator, you can stop a running session with this command.\n" +
            "You can review the payouts of a running or stopped session.\n")]
        [RequireUserPermission(ChannelPermission.ManageMessages)]
        public async Task Stop()
        {
            var dbUser = await UserUtils.GetOrUpdateUser(db, config, Context.User.Id, Context.User.ToString()).ConfigureAwait(false);
            var output = new EmbedBuilder();

            await Discord.SendAsync(Context, config.ChannelId, output).ConfigureAwait(false);
        }
    }
}