using Posting.Service.Models;
using static Posting.Service.DTOs;

namespace Posting.Service
{
    public static class Extensions
    {
        public static JobDTO AsDTO(this Job job)
        {
            return new JobDTO(job.Id, job.Title, job.Location, job.Description, job.FullOrPartTime, job.MonthlySalary, job.Currency);
        }
    }
}