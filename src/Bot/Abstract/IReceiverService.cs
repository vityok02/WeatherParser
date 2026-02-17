namespace Bot.Abstract;

public interface IReceiverService
{
    Task ReceiveAsync(CancellationToken cancellationToken);
}
