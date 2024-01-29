namespace WeatherParser.Handlers.CallbackQueryHandlers;

public interface ICallbackQueryHandlerFactory
{
    ICallbackQueryHandler GetHandler(string query);
}