using WeatherParser.Features.UserState.Constants;

namespace WeatherParser.Features.UserState.UserStateHandlers;

public interface IUserStateHandlerFactory
{
    IUserStateHandler GetUserStateHandler(string userState);
}

public class UserStateHandlerFactory : IUserStateHandlerFactory
{
    private readonly IServiceProvider _serviceProvider;

    public UserStateHandlerFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IUserStateHandler GetUserStateHandler(string userState)
    {
        return userState switch
        {
            UserStates.EnterLocation => _serviceProvider.GetRequiredService<EnterLocationStateHandler>(),
            UserStates.SetLocation => _serviceProvider.GetRequiredService<SetLocationStateHandler>(),
            _ => default!
        };
    }
}
