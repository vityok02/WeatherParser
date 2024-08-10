using Domain.Locations;

namespace Application.Messaging;

public interface IMessage
{
    long UserId { get; }
    string MessageText { get; }
    Coordinates? Coordinates { get; }
}
