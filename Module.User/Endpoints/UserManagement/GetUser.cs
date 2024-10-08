﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Shared.Abstractions;
using Module.User.Application.Features.UserManagement.Query;
using Module.User.Application.Features.UserManagement.Query.Dto;

namespace Module.User.Endpoints;

public class GetUser : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app)
    {
        app.MapGet("User/{id}", async ([FromRoute]Guid id, [FromServices]IMediator mediator) 
                => await mediator.Send(new GetUserQuery(id)))
            .WithTags("UserManagement")
            .Produces<UserResponse>();
    }
}