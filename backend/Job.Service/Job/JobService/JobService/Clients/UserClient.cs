using JobService.Core.Dtos;
using Microsoft.AspNetCore.Authorization;
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
            var user = await httpClient.GetFromJsonAsync<UserDto>("api/Auth/GetloggedInUser");
            return user;
        }
    }
}
