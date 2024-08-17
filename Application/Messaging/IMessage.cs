using Domain.Locations;

namespace Application.Messaging;

public interface IMessage
{
    long UserId { get; }
    string Text { get; }
    Coordinates? Coordinates { get; }
}
