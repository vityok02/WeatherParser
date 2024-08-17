﻿using Application.Common.Abstract;
using Application.Common.Interfaces.ReplyMarkup;
using Application.Commands.Locations.EnterPlaceName;
using Application.Locations.SetLocationFromRequest;
using Domain.Abstract;
using Domain.Locations;
using Domain.Users;
using MediatR;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Messaging;

namespace Application.Commands.Locations.SetLocation;

internal sealed class SetLocationCommandHandler : ICommandHandler<SetLocationCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly IUserRepository _userRepository;
    private readonly IUserStateRepository _userStateRepository;
    private readonly IPlacesRepository _placesRepository;
    private readonly ISender _sender;
    private readonly IRemoveKeyboardMarkup _replyMarkup;

    public SetLocationCommandHandler(
        IMessageSender messageSender,
        IUserRepository userRepository,
        IUserStateRepository cachedUserStateRepository,
        IPlacesRepository cachedPlacesRepository,
        ISender sender,
        IRemoveKeyboardMarkup replyMarkup)
    {
        _messageSender = messageSender;
        _userRepository = userRepository;
        _userStateRepository = cachedUserStateRepository;
        _placesRepository = cachedPlacesRepository;
        _sender = sender;
        _replyMarkup = replyMarkup;
    }

    public async Task<Result> Handle(SetLocationCommand command, CancellationToken cancellationToken)
    {
        var locations = _placesRepository.GetPlaces(command.UserId);

        if (locations is null)
        {
            return await _sender
                .Send(new EnterPlaceNameCommand(command.UserId, command.PlaceName), cancellationToken);
        }

        var selectedLocation = locations.FirstOrDefault(l => l.PlaceName == command.PlaceName);

        if (selectedLocation is null)
        {
            return await _sender
                .Send(new EnterPlaceNameCommand(command.UserId, command.PlaceName), cancellationToken);
        }

        var locationToSet = selectedLocation.ToAppLocation();

        var user = await _userRepository.GetByIdWithLocationsAsync(command.UserId, cancellationToken);
        user!.SetCurrentLocation(locationToSet);

        _userStateRepository.RemoveState(command.UserId);

        await _userRepository.SaveChangesAsync(cancellationToken);

        await _messageSender.SendTextMessageAsync(
            chatId: command.UserId,
            text: "Location successfully set ✅",
            replyMarkup: _replyMarkup,
            cancellationToken: cancellationToken);

        return Result.Success();
    }
}
