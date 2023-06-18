namespace JobService.Core.Dtos
{
    public record UserDto(Guid Id, string username, string fullname, string email, string phone, string address, string profession, string skills, string bio, string role);
}
