using Application.Common.Interfaces.Messaging;
using Domain.Locations;
using Telegram.Bot.Types;
using MessageLocation = Telegram.Bot.Types.Location;

namespace Bot.Messages;

public class TelegramMessageAdapter : IMessage
{
    private readonly Message _message;

    public TelegramMessageAdapter(Message message)
    {
        _message = message;
    }

    public long UserId => _message.From!.Id;
    public string Text => _message.Text!;
    public Coordinates? Coordinates
    {
        get
        {
            if (_message.Location is MessageLocation location)
            {
                return new Coordinates(location.Latitude, location.Longitude);
            }
            return null;
        }
    }
}
