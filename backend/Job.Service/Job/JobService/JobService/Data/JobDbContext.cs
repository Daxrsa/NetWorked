using JobService.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace JobService.Data
{
    public class JobDbContext : DbContext
    {
        public JobDbContext(DbContextOptions<JobDbContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<JobPosition>()
                .HasOne(job => job.Company)
                .WithMany(company => company.jobPositions)
                .HasForeignKey(job => job.CompanyId);

            modelBuilder.Entity<JobPosition>()
                .HasOne(job => job.JobCategory)
                .WithMany(category => category.jobPositions)
                .HasForeignKey(job => job.CategoryId);

            modelBuilder.Entity<Application>()
                .HasOne(application => application.JobPosition)
                .WithMany(job => job.Applications)
                .HasForeignKey(application => application.JobId);

            modelBuilder.Entity<Company>()
                .Property(company => company.Size)
                .HasConversion<string>();

            modelBuilder.Entity<Company>()
                .Property(company => company.CityLocation)
                .HasConversion<string>();

            modelBuilder.Entity<JobPosition>()
                .Property(job => job.JobLevel)
                .HasConversion<string>();

        }
    }
}
