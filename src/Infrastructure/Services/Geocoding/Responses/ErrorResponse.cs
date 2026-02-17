namespace Infrastructure.Services.Geocoding.Responses;

public record ErrorResponse(string Message, string Error, int StatusCode);
