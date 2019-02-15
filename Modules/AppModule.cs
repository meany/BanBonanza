using Discord;
using Discord.Commands;
using dm.BanBonanza.Data;
using Microsoft.Extensions.Options;
using NLog;
using System;
using System.Threading.Tasks;

namespace dm.BanBonanza.Modules
{
    public class AppModule : ModuleBase
    {
        private readonly Config config;
        private readonly AppDbContext db;
        private static Logger log = LogManager.GetCurrentClassLogger();

        public AppModule(IOptions<Config> config, AppDbContext db)
        {
            this.config = config.Value;
            this.db = db;
        }

        [Command("info")]
        [Alias("stats")]
        [Summary("Shows information and statistics")]
        [Remarks("You can review session, game, and guess information with this command.\n" +
            "You can also show specific information about a session and game with the optional parameters.\n")]
        public async Task Start(int sessionId = 0, int gameId = 0)
        {
            try
            {
                var dbUser = await UserHelper.GetUser(db, config, Context.User.Id, Context.User.ToString()).ConfigureAwait(false);
                var output = new EmbedBuilder();

                string title = $"BanBonanza statistics";
                string body = "not implemented <:smug:543520078747140106>";
                output.WithColor(Color.BOT)
                .WithAuthor(author =>
                {
                    author.WithName(title);
                })
                .WithDescription(body);

                if (gameId == 0)
                {
                    if (sessionId == 0)
                    {
                        // GLOBAL STATS
                    }
                    else
                    {
                        // SESSION STATS
                    }
                }
                else
                {
                    // GAME STATS
                }

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