using System.Security.Claims;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SportsRidingClubSkovly.Web.Services.Interface;
using SportsRidingClubSkovly.Web.ViewModels;
using IAuthenticationService = SportsRidingClubSkovly.Web.Services.Interface.IAuthenticationService;

namespace SportsRidingClubSkovly.Web.Components.Pages.Account;

public partial class SignUp : ComponentBase
{
    [CascadingParameter] public HttpContext? HttpContext { get; set; }
    
    [SupplyParameterFromForm]
    public SignUpViewModel Model { get; set; } = new SignUpViewModel();
    
    [Inject] public IAuthenticationService AuthenticationService { get; set; }

    private string? errorMessage;

    private async Task SignUpUser()
    {
        await AuthenticationService.SignUpAsync(Model);
    }
}
