using Domain.Locations;

namespace Application;

public record MessageContext(long UserId, string MessageText, Coordinates? Coordinates);
