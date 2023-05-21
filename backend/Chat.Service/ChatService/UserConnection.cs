using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http;

namespace ChatService
{
    public class UserConnection
    {
        public string User { get; set; }

        public async Task<string> GetUserAsync()
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjQ2YzU2NTM3LWU5M2YtNDhkMy0zZWExLTA4ZGI1OWY5ODhkZiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJlcm9zZW1yaSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNjg0Nzc2NDkwfQ.aWi5gSw8XUlllXc4DpW6AzaxtTs2x7dmroU6PRZYyPFL6Szqu4qcvSU_bTQ1N4Aoa0C60ppMak6M7cc-Gyco_A");

            var response = await client.GetAsync("https://localhost:7212/api/Auth/Getloggedinuser");

            response.EnsureSuccessStatusCode();
            User = await response.Content.ReadAsStringAsync();

            return User;
        }

        public string Room { get; set; }
    }
}
