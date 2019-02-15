using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace dm.BanBonanza
{
    public static class Extensions
    {
        internal static string Format(this int source)
        {
            return string.Format("{0:#,##0}", source);
        }

        internal static string Format(this decimal source)
        {
            return string.Format("{0:#,##0}", source);
        }

        internal static string ToDate(this DateTime source)
        {
            return source.ToString("r");
        }

        internal static string TrimEnd(this string source, string suffixToRemove, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
        {

            if (source != null && suffixToRemove != null && source.EndsWith(suffixToRemove, comparisonType))
            {
                return source.Substring(0, source.Length - suffixToRemove.Length);
            }
            else
            {
                return source;
            }
        }

        internal static IServiceCollection AddDatabase<T>(this IServiceCollection services, string connectionString) where T : DbContext
        {
            services.AddDbContext<T>(options => options.UseSqlServer(connectionString));
            return services;
        }
    }
}
