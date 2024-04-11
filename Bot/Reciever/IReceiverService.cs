namespace Bot.Reciever;

public interface IReceiverService
{
    Task ReceiveAsync(CancellationToken cancellationToken);
}
