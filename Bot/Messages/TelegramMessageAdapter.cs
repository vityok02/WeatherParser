using Application.Messaging;
using Domain.Locations;
using Telegram.Bot.Types;

namespace Bot.Messages;

public class TelegramMessageAdapter : IMessage
{
    private readonly Message _message;

    public TelegramMessageAdapter(Message message)
    {
        _message = message;
    }

    public long UserId => _message.From!.Id;
    public string MessageText => _message.Text!;
    public Coordinates? Coordinates
    {
        get
        {
            if (_message.Location is Telegram.Bot.Types.Location location)
            {
                return new Coordinates(location.Latitude, location.Longitude);
            }
            return null;
        }
    }
}
