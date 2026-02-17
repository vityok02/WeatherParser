using Domain.Locations;

namespace Application.Common.Interfaces.Messaging;

public interface IMessage
{
    long UserId { get; }
    string Text { get; }
    Coordinates? Coordinates { get; }
}
