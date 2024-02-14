using Microsoft.Extensions.Options;

namespace Bot;

public static class PollingExtensions
{
    public static T GetConfiguration<T>(this IServiceProvider sp)
        where T : class
    {
        var o = sp.GetService<IOptions<T>>();
        if (o is null)
        {
            throw new ArgumentNullException(nameof(T));
        }

        return o.Value;
    }
}
