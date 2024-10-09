﻿using MediatR;
using Module.User.Application.Abstractions;
using Module.User.Application.Features.TrainerSession.Command.Dto;
using Module.User.Domain.Entity;

namespace Module.User.Application.Features.TrainerSession.Command;

public record CreateSessionCommand (
    CreateSessionRequest createSessionRequest) : IRequest;


internal class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand>
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IUserRepository _trainerRepository;

    public CreateSessionCommandHandler(ISessionRepository sessionRepository, IUserRepository trainerRepository)
    {
        _sessionRepository = sessionRepository;
        _trainerRepository = trainerRepository;
    }

    public async Task Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        // Load
        var trainer = await _trainerRepository.GetTrainerById(request.createSessionRequest.AssignedTrainerId);

        // Do
        var session = Session.Create(
            request.createSessionRequest.StartTime,
            request.createSessionRequest.EndTime, 
            trainer, 
            request.createSessionRequest.AvailableSlots, 
            request.createSessionRequest.DifficultyLevel, 
            request.createSessionRequest.Type);

        // Save
        await _sessionRepository.AddSessionAsync(session);  
    }
}