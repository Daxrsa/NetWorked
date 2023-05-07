using Job.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Job.Configuration
{
    public class JobDbContext : DbContext
    {
        public JobDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<JobPositon> JobPositions { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
    }
     
}
