namespace Recruiter.Service
{
    public class DTOs
    {
        public record JobDTO
        (
            Guid Id,
            string Title,
            string Description,
            string Location,
            string FullOrPartTime,
            decimal MonthlySalary,
            string Currency
        );

        public record CreateJobDT
        (
            Guid Id,
            string Title,
            string Description,
            string Location,
            string FullOrPartTime,
            decimal MonthlySalary,
            string Currency
        );

        public record UpdateJobDTO
        (
            Guid Id,
            string Title,
            string Description,
            string Location,
            string FullOrPartTime,
            decimal MonthlySalary,
            string Currency
        );
    }
}