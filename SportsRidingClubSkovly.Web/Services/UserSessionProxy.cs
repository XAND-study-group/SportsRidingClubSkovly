﻿using SportsRidingClubSkovly.Web.DTO.UserSession;
using SportsRidingClubSkovly.Web.Services.Interface;

namespace SportsRidingClubSkovly.Web.Services
{
    public class UserSessionProxy : IUserSessionProxy
    {
        private readonly HttpClient _httpClient;

        public UserSessionProxy(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("API");
        }

        async Task<IEnumerable<SessionResponse>> IUserSessionProxy.GetSessionsAsync()
        {
            var response = await _httpClient.GetAsync("Sessions");
            return await response.Content.ReadFromJsonAsync<IEnumerable<SessionResponse>>() ?? [];
        }
    }
}