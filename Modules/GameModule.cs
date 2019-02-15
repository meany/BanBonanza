using Discord;
using Discord.Commands;
using dm.BanBonanza.Data;
using Microsoft.Extensions.Options;
using NLog;
using System;
using System.Threading.Tasks;

namespace dm.BanBonanza.Modules
{
    public class GameModule : ModuleBase
    {
        private readonly Config config;
        private readonly AppDbContext db;
        private static Logger log = LogManager.GetCurrentClassLogger();

        public GameModule(IOptions<Config> config, AppDbContext db)
        {
            this.config = config.Value;
            this.db = db;
        }

        [Command("go")]
        [Alias("start")]
        [Summary("Starts a new session")]
        [Remarks("If you are a game administrator, you can start a new session with this command.\n" +
            "Sessions will run until the pot is empty or until you use the **stop** command.\n")]
        [RequireUserPermission(ChannelPermission.ManageMessages)]
        public async Task Start(int pot, decimal difficulty = 0, [Remainder]string name = "BanBonanza")
        {
            try
            {
                var dbUser = await UserHelper.GetUser(db, config, Context.User.Id, Context.User.ToString()).ConfigureAwait(false);

                if (await GameHelper.SessionStarted(db).ConfigureAwait(false))
                {
                    await DiscordHelper.ReplyAsync(Context, message: "Session already running.").ConfigureAwait(false);
                    return;
                }

                var session = await GameHelper.CreateSession(db, dbUser, pot, name, difficulty).ConfigureAwait(false);

                string title = $"BanBonanza";
                string body = $"Session #**{session.SessionId}** started by {Context.User.Mention}.\n" +
                    $"The first game will start in 60 seconds! Get ready!!\n" +
                    $"Pot is set to **{session.Pot.Format()}**, and difficuly to **{difficulty}**.";

                var output = new EmbedBuilder();
                output.WithColor(Color.BOT)
                .WithAuthor(author =>
                {
                    author.WithName(title);
                })
                .WithDescription(body)
                .WithFooter(footer =>
                {
                    footer.WithText($"The next game starts at {session.Start.AddMinutes(1).ToDate()}")
                        .WithIconUrl(Asset.CLOCK);
                });

                await DiscordHelper.ReplyAsync(Context, output).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw;
            }
        }

        [Command("stop")]
        [Summary("Stops current session")]
        [Remarks("If you are a game administrator, you can stop a running session with this command.\n" +
            "You can review the payouts of a running or stopped session.\n")]
        [RequireUserPermission(ChannelPermission.ManageMessages)]
        public async Task Stop()
        {
            try
            {
                var dbUser = await UserHelper.GetUser(db, config, Context.User.Id, Context.User.ToString()).ConfigureAwait(false);

                if (!await GameHelper.SessionStarted(db).ConfigureAwait(false))
                {
                    await DiscordHelper.ReplyAsync(Context, message: "No session running.").ConfigureAwait(false);
                    return;
                }

                var session = await GameHelper.StopSession(db).ConfigureAwait(false);

                string title = $"BanBonanza";
                string body = $"Session #**{session.SessionId}** stopped by {Context.User.Mention}.\n";

                var output = new EmbedBuilder();
                output.WithColor(Color.BOT)
                .WithAuthor(author =>
                {
                    author.WithName(title);
                })
                .WithDescription(body);

                await DiscordHelper.ReplyAsync(Context, output).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw;
            }
        }
    }
}