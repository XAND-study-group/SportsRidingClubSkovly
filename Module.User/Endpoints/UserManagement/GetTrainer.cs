﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Shared.Abstractions;
using Module.User.Application.Features.UserManagement.Query;

namespace Module.User.Endpoints.UserManagement;

public class GetTrainer : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app)
    {
        app.MapGet("Trainer/{id}", async ([FromRoute] Guid id, [FromServices] IMediator mediator)
                => await mediator.Send(new GetTrainerQuery(id)))
            .WithTags("UserManagement");
    }
}