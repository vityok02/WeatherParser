using Domain.Abstract;

namespace Infrastructure.Services;

public static class ClientErrors
{
    public static readonly Error HttpResponseError = new(
        "Service.NullResponse",
        "Could not get response");

    public static readonly Error JsonError = new(
        "Deserialization.Failed",
        "Failed to deserialize object");

    public static readonly Error UnexpectedError = new(
        "Service.UnexpectedError",
        "An unexpected error occured");
}
