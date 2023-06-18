using JobService.Core.Dtos;

namespace JobService.Client
{
    public interface IClient
    {
        Task<UserDto> GetUserAsync(string authorizationHeader);
    }
}
