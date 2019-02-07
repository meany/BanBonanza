using Discord;
using Discord.Commands;
using Discord.WebSocket;
using dm.BanBonanza.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NLog;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace dm.BanBonanza
{
    public class Program
    {
        private CommandService commands;
        private DiscordSocketClient client;
        private IServiceProvider services;
        private IConfigurationRoot configuration;
        private Config config;
        private AppDbContext db;
        private static Logger log = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
            => new Program().MainAsync(args).GetAwaiter().GetResult();

        public async Task MainAsync(string[] args)
        {
            log.Debug(Utils.GetCurrentClassAndMethod());
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("Config.Bot.json", optional: true, reloadOnChange: true)
                    .AddJsonFile("Config.Bot.Local.json", optional: true, reloadOnChange: true);

                configuration = builder.Build();

                client = new DiscordSocketClient(new DiscordSocketConfig
                {
                    MessageCacheSize = 100
                });
                client.Log += Log;

                services = new ServiceCollection()
                    .Configure<Config>(configuration)
                    .AddDatabase<AppDbContext>(configuration.GetConnectionString("Database"))
                    .BuildServiceProvider();
                config = services.GetService<IOptions<Config>>().Value;
                db = services.GetService<AppDbContext>();

                if (args.Length > 0)
                {
                    await RunArgs(args).ConfigureAwait(false);
                }
                else
                {
                    await RunBot().ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private async Task RunArgs(string[] args)
        {
            log.Debug(Utils.GetCurrentClassAndMethod());
            try
            {
                await Start().ConfigureAwait(false);
                var handle = new Args(client, config, db);
                switch (args[0])
                {
                    case "-blank-":
                        if (int.TryParse(args[1], out int depositId) && int.TryParse(args[2], out int returnId1))
                        {
                            await handle.Deposit(depositId, returnId1).ConfigureAwait(false);
                        }
                        else
                        {
                            log.Warn($"Could not parse DepositId '{args[1]}' or ReturnId '{args[2]}'");
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private async Task RunBot()
        {
            log.Debug(Utils.GetCurrentClassAndMethod());
            try
            {
                commands = new CommandService();

                await Install().ConfigureAwait(false);
                await Start().ConfigureAwait(false);
                await client.SetGameAsync($"Bonanza | {config.BotPrefix}help").ConfigureAwait(false);

                await Task.Delay(-1).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private Task Log(LogMessage msg)
        {
            log.Info(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task Install()
        {
            log.Debug(Utils.GetCurrentClassAndMethod());
            try
            {
                var events = new Events(commands, client, services, config, db);
                client.Connected += events.HandleConnected;
                client.MessageReceived += events.HandleCommand;
                //client.ReactionAdded += events.HandleReaction;
                await commands.AddModulesAsync(Assembly.GetEntryAssembly(), services).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private async Task Start()
        {
            log.Debug(Utils.GetCurrentClassAndMethod());
            try
            {
                await client.LoginAsync(TokenType.Bot, config.BotToken).ConfigureAwait(false);
                await client.StartAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }


}