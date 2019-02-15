using dm.BanBonanza.Data;
using dm.BanBonanza.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace dm.BanBonanza
{
    public static class GameHelper
    {
        internal static async Task<bool> SessionStarted(AppDbContext db)
        {
            return await db.Sessions
                .AsNoTracking()
                .AnyAsync(x => x.End == DateTime.MinValue)
                .ConfigureAwait(false);
        }

        internal static async Task<Session> CreateSession(AppDbContext db, User user, int pot, string name, decimal difficulty)
        {
            var item = new Session
            {
                Difficulty = difficulty,
                End = DateTime.MinValue,
                Name = name,
                Pot = pot,
                Start = DateTime.UtcNow,
                StartedById = user.UserId
            };
            db.Sessions.Add(item);
            await db.SaveChangesAsync().ConfigureAwait(false);
            return item;
        }

        internal static async Task<Session> StopSession(AppDbContext db)
        {
            var item = await db.Sessions
                .SingleOrDefaultAsync(x => x.End == DateTime.MinValue)
                .ConfigureAwait(false);
            item.End = DateTime.UtcNow;
            //db.Sessions.Update(item);
            await db.SaveChangesAsync().ConfigureAwait(false);
            return item;
        }
    }
}
