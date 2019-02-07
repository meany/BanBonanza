using Discord;
using Discord.WebSocket;
using dm.BanBonanza.Data;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace dm.BanBonanza
{
    public class Args
    {
        private readonly DiscordSocketClient client;
        private readonly Config config;
        private readonly AppDbContext db;
        private static Logger log = LogManager.GetCurrentClassLogger();

        public Args(DiscordSocketClient client, Config config, AppDbContext db)
        {
            this.client = client;
            this.config = config;
            this.db = db;
        }

        public async Task Deposit(int depositId, int returnId)
        {
            log.Debug(Utils.GetCurrentClassAndMethod());
            try
            {
                //
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }
}