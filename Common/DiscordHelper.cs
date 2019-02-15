using Discord;
using Discord.Commands;
using System;
using System.IO;
using System.Threading.Tasks;

namespace dm.BanBonanza
{
    public static class DiscordHelper
    {
        internal static async Task ReplyAsync(ICommandContext ctx, EmbedBuilder embed = null, Stream file = null, string fileName = null, string message = "", bool deleteUserMessage = false)
        {
            var channel = ctx.Channel;
            if (file != null && fileName != null && embed != null)
            {
                await channel.SendFileAsync(file, fileName, embed: embed.Build()).ConfigureAwait(false);
            }
            else if (embed != null)
            {
                await channel.SendMessageAsync(message, embed: embed.Build()).ConfigureAwait(false);
            }
            else
            {
                await channel.SendMessageAsync(message).ConfigureAwait(false);
            }

            if (deleteUserMessage && ctx.Guild != null)
            {
                await ctx.Message.DeleteAsync().ConfigureAwait(false);
            }
        }

        internal static async Task ReplyDMAsync(ICommandContext ctx, EmbedBuilder embed = null, Stream file = null, string fileName = null, string message = "", bool deleteUserMessage = true)
        {
            await SendDMAsync(ctx.User, embed, file, fileName, message);
            if (deleteUserMessage && ctx.Guild != null)
            {
                await ctx.Message.DeleteAsync().ConfigureAwait(false);
            }
        }

        internal static async Task SendAsync(ICommandContext ctx, ulong channelId, EmbedBuilder embed = null, Stream file = null, string fileName = null, string message = "")
        {
            var channel = (ITextChannel)await ctx.Client.GetChannelAsync(channelId).ConfigureAwait(false);
            if (file != null && fileName != null && embed != null)
            {
                await channel.SendFileAsync(file, fileName, embed: embed.Build()).ConfigureAwait(false);
            }
            else if (embed != null)
            {
                await channel.SendMessageAsync(message, embed: embed.Build()).ConfigureAwait(false);
            }
            else
            {
                await channel.SendMessageAsync(message).ConfigureAwait(false);
            }
        }

        internal static async Task SendDMAsync(IUser user, EmbedBuilder embed = null, Stream file = null, string fileName = null, string message = "")
        {
            var channel = await user.GetOrCreateDMChannelAsync().ConfigureAwait(false);
            if (file != null && fileName != null && embed != null)
            {
                await channel.SendFileAsync(file, fileName, embed: embed.Build()).ConfigureAwait(false);
            }
            else if (embed != null)
            {
                await channel.SendMessageAsync(message, embed: embed.Build()).ConfigureAwait(false);
            }
            else
            {
                await channel.SendMessageAsync(message).ConfigureAwait(false);
            }
        }
    }
}
