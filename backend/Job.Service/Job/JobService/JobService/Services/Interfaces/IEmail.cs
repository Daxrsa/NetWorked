namespace JobService.Services.Interfaces
{
    public interface IEmail
    {
        string SendEmail(string email, string username, string job);
    }
}
