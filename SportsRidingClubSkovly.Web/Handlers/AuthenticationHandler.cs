using System.Net.Http.Headers;
using SportsRidingClubSkovly.Web.Services.Interface;

namespace SportsRidingClubSkovly.Web.Handlers;

public class AuthenticationHandler : DelegatingHandler
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IJwtService _jwtService;
    private readonly IConfiguration _configuration;


    public AuthenticationHandler(IAuthenticationService authenticationService, IJwtService jwtService, IConfiguration configuration)
    {
        _authenticationService = authenticationService;
        _jwtService = jwtService;
        _configuration = configuration;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var jwt = await _authenticationService.GetJwtAsync();
        var isToServer = request.RequestUri?.AbsolutePath.StartsWith("http://localhost:8080") ?? false;

        if (isToServer && !string.IsNullOrEmpty(jwt))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        
        return await base.SendAsync(request, cancellationToken);
    }
}
