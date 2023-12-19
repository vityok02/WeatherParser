namespace WeatherParser.Abstract;

public interface IReceiverService
{
    Task ReceiveAsync(CancellationToken cancellationToken);
}
