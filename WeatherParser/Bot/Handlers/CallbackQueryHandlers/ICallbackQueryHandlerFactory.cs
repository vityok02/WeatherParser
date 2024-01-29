namespace WeatherParser.Bot.Bot.Handlers.CallbackQueryHandlers;

public interface ICallbackQueryHandlerFactory
{
    ICallbackQueryHandler GetHandler(string query);
}