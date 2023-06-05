namespace JobService.Core.Dtos
{
    public record UserDto(Guid Id, string username);

    public record Token(string data);
}
