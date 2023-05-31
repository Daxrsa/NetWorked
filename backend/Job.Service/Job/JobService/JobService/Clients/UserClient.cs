using JobService.Core.Dtos;
using System.Net.Http.Headers;

namespace JobService.Clients
{
    public class UserClient
    {
        private readonly HttpClient httpClient;

        public UserClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<UserDto> GetUserAsync()
        {
            //httpClient.DefaultRequestHeaders.Authorization = new AuthorizationHeaderValue("Bearer", "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjY5MmVmYTYyLTVhM2QtNDI1My1lOTE2LTA4ZGI1YWMwYTVkYyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJmaXRvcmV0YWhpcmkiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTY4NTQ4NjY5NH0.JmkIhENQAT_ZyXJnjFg49Xx6nseCvKE39hRWJF5yL22wV_1XixFYXqi9ujwS4Wf2BuKYVTAl3ytJDfwEYBq55A");
            var user = await httpClient.GetFromJsonAsync<UserDto>("api/Auth/GetloggedInUser");
            return user;
        }

        public async Task<Token> GetTokenAsync()
        {
            var token = await httpClient.GetFromJsonAsync<Token>("/login");
            return token;
        }
    }
}
