﻿namespace Module.User.Application.Features.UserSession.Command.Dto;

public record DeleteBookingRequest(
    Guid BookingId,
    Guid SessionId,
    byte[] RowVersion);