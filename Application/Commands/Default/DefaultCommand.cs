﻿using Application.Common.Abstract;

namespace Application.Commands.Default;

public record DefaultCommand(long ChatId) : ICommand;