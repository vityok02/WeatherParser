using Microsoft.Extensions.Options;

namespace Bot.Extensions;

public static class PollingExtensions
{
    public static T GetConfiguration<T>(this IServiceProvider sp)
        where T : class
    {
        return sp.GetRequiredService<IOptions<T>>().Value;
    }
}
