using Blazored.SessionStorage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SportsRidingClubSkovly.Web.Services.Interface;
using SportsRidingClubSkovly.Web.ViewModels;
using IAuthenticationService = SportsRidingClubSkovly.Web.Services.Interface.IAuthenticationService;

namespace SportsRidingClubSkovly.Web.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IAccountProxy _accountProxy;
    private readonly ISessionStorageService _sessionStorageService;
    private readonly NavigationManager _navigationManager;
    private readonly IJSRuntime _jsRuntime;

    public AuthenticationService(IAccountProxy accountProxy, ISessionStorageService sessionStorageService, NavigationManager navigationManager, IJSRuntime jsRuntime)
    {
        _accountProxy = accountProxy;
        _sessionStorageService = sessionStorageService;
        _navigationManager = navigationManager;
        _jsRuntime = jsRuntime;
    }
    
    async Task IAuthenticationService.LoginAsync(LoginViewModel viewModel)
    {
        var user = await _accountProxy.AuthenticateUser(viewModel.Username, viewModel.Password);
        
        var token = user.Token; 
        
        await SaveJwtToken(token);
        
        _navigationManager.NavigateTo("/"); 
    }

    async Task IAuthenticationService.SignUpAsync(SignUpViewModel viewModel)
    {
        var user = await _accountProxy.SignUpUser(
            viewModel.Username, 
            viewModel.Password,
            viewModel.FirstName, 
            viewModel.LastName, 
            viewModel.Phone, 
            viewModel.Username);

        if (user is null)
            return;
        
        var token = user.Token; 
        
        await SaveJwtToken(token);
        
        _navigationManager.NavigateTo("/"); 
    }

    async Task IAuthenticationService.LogoutAsync()
    {
        await _sessionStorageService.RemoveItemAsync("jwt_token");
        _navigationManager.NavigateTo("/login"); 
    }

    async Task<string> IAuthenticationService.GetJwtAsync()
    {
        return await GetJwtToken();
    }

    private async Task SaveJwtToken(string token)
    {
        await _sessionStorageService.SetItemAsync("jwt_token", token);
        await _jsRuntime.InvokeVoidAsync("blazorExtensions.SaveJwtToken", token);
    }

    private async Task<string> GetJwtToken()
    {
        var token = await _sessionStorageService.GetItemAsync<string>("jwt_token");
        if (string.IsNullOrEmpty(token))
        {
            token = await _jsRuntime.InvokeAsync<string>("blazorExtensions.GetJwtToken");
        }
        return token;
    }
}
