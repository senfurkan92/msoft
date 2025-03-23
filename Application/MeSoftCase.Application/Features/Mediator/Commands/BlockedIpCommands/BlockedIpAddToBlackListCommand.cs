﻿using MediatR;

namespace MeSoftCase.Application.Features.Mediator.Commands.BlockedIpCommands
{
    /// <summary>
    /// add ipAddress on blacklist
    /// </summary>
    /// <param name="ipAddress"></param>
    public record BlockedIpAddToBlackListCommand(string ipAddress) : IRequest;
}
