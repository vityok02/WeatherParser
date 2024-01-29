using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using NSubstitute;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using WeatherParser.Data.Repositories.CacheRepositories;
using WeatherParser.Handlers.UserStateHandlers;
using WeatherParser.Models;
using WeatherParser.Models.GeocodingRecords;
using WeatherParser.Models.Interfaces;
using WeatherParser.Services.GeocodingServices;

namespace WeatherParser.Tests;

public class EnterLocationStateHandlerTests
{
    private readonly IGeocodingService _geocodingService = Substitute.For<IGeocodingService>();
    private readonly IMessageSenderForLocationService _messageSender = Substitute.For<IMessageSenderForLocationService>();
    private readonly LocationsCacheRepository _locationCacheRepository = Substitute.For<LocationsCacheRepository>(Substitute.For<IMemoryCache>());
    private readonly IUserStateService _userStateService = Substitute.For<IUserStateService>();
    private EnterLocationStateHandler _enterLocationStateHandler => new(
        _geocodingService,
        _messageSender,
        _locationCacheRepository,
        _userStateService);

    [Fact]
    public async Task Handle_WithText_ReturnSendSelectLocationMessage()
    {
        //Arrange
        long userId = 1;
        string text = "Vinnytsa";

        _geocodingService
            .GetLocationsByName(text)
            .Returns(Result<Feature[]>.Success([]));

        _messageSender.
            SendSelectLocationMessage(Arg.Any<long>(), Arg.Any<ReplyKeyboardMarkup>())
            .ReturnsForAnyArgs(new Message());

        //Act
        var result = await _enterLocationStateHandler.HandleAsync(userId, text);

        //Assert
        result.Should().BeOfType<Message>();
        await _messageSender.Received(1).SendSelectLocationMessage(userId, Arg.Any<ReplyKeyboardMarkup>());
    }
}