using JobService.Core.Dtos;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;

namespace JobService.Client
{
    public class ClientService : IClient
    {
        private readonly HttpClient _httpClient;
        public ClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<UserDto> GetUserAsync(string authorizationHeader)
        {
            var token = authorizationHeader.Split(' ')[1];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var userResponse = await _httpClient.GetAsync("http://localhost:5116/api/Auth/GetloggedInUser");
            UserDto? loggedInUser = null;
            if (userResponse != null)
            {
                string content = await userResponse.Content.ReadAsStringAsync();
                loggedInUser = JsonConvert.DeserializeObject<UserDto>(content);

            }
            return loggedInUser;
        }
    }
}
