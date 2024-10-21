using System.Text;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SportsRidingClubSkovly.Web.Components;
using SportsRidingClubSkovly.Web.Handlers;
using SportsRidingClubSkovly.Web.Services;
using SportsRidingClubSkovly.Web.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//     .AddCookie(option =>
//     {
//         option.Cookie.Name = "auth_token";
//         option.LoginPath = "/login";
//         option.Cookie.MaxAge = TimeSpan.FromDays(7);
//         option.AccessDeniedPath = "/access-denied";
//     });


// Register your custom AuthenticationStateProvider
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options 
        =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "your_token_issuer", // Replace with your issuer
            ValidAudience = "your_token_audience", // Replace with your audience
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key")) // Replace with your secret key
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddHttpClient("API", httpClient =>
{
    httpClient.BaseAddress = new Uri("http://localhost:8080");
})
.AddHttpMessageHandler<AuthenticationHandler>();

builder.Services.AddScoped<IUserManagementProxy, UserManagementProxy>();
builder.Services.AddScoped<IAccountProxy, AccountProxy>();
builder.Services.AddScoped<IUserSessionProxy, UserSessionProxy>();
builder.Services.AddScoped<AuthenticationHandler>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddBlazoredSessionStorage();

builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options => 
    {
        options.DetailedErrors = true;
        options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(3);
        options.JSInteropDefaultCallTimeout = TimeSpan.FromMinutes(1);
    });

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
