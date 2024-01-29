using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Telegram.Bot.Types;

namespace WeatherParser.Models.Interfaces;

public interface IMyInterface
{
    Task<Message> SendLocationsListAsync(long userId, string text, CancellationToken cancellationToken = default);
}