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
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjI3NTk5NTYwLWI2MjYtNGY0OC1kM2U5LTA4ZGI1YWZlNGVlNyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJlcm9zaTEyM2EiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTY4NDg3MTY3NH0.IxqR3z7rLJ5foeeJ8Mzpr-9P50zv--a0JKjzHRq_Yx19TUQ8Qi3cE7FBbt6dWcWPCkv4Hy_EpcM2Gc4dhoIpTQ");

            var response = await client.GetAsync("https://localhost:7212/api/Auth/Getloggedinuser");

            response.EnsureSuccessStatusCode();
            User = await response.Content.ReadAsStringAsync();

            return User;
        }

        public string Room { get; set; }
    }
}
