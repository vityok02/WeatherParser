namespace Infrastructure.Services.Geocoding.Response;

public record ErrorResponse(string Message, string Error, int StatusCode);
