using dm.BanBonanza.Data;
using dm.BanBonanza.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace dm.BanBonanza
{
    public static class UserHelper
    {
        internal static async Task<User> GetUser(AppDbContext db, Config config, ulong userId, string userName)
        {
            var item = await GetUser(db, userId).ConfigureAwait(false);
            if (item == null)
            {
                item = await CreateUser(db, config, userId, userName).ConfigureAwait(false);
            }
            return item;
        }

        private static async Task<User> GetUser(AppDbContext db, ulong userId)
        {
            return await db.Users
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .Include(x => x.SessionsStarted)
                .Include(x => x.GamesWon)
                .Include(x => x.Guesses)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        private static async Task<User> CreateUser(AppDbContext db, Config config, ulong userId, string userName)
        {
            var user = new User
            {
                UserId = userId,
                Name = userName,
            };
            db.Users.Add(user);
            await db.SaveChangesAsync().ConfigureAwait(false);
            return await GetUser(db, userId).ConfigureAwait(false);
        }
    }
}
