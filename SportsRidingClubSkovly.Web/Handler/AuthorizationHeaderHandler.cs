﻿using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace SportsRidingClubSkovly.Web.Handler;

public class AuthorizationHeaderHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationHeaderHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var absolutePath = request.RequestUri.AbsolutePath;
        if (absolutePath.Contains("Login") || absolutePath.Contains("SignUp"))
            return await base.SendAsync(request, cancellationToken);
        // Get the JWT token from the provider
        var user = _httpContextAccessor.HttpContext.User;
        var token = user.FindFirst("JWT")?.Value;

        // Add the JWT token to the Authorization header
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Call the inner handler
        return await base.SendAsync(request, cancellationToken);
    }
}