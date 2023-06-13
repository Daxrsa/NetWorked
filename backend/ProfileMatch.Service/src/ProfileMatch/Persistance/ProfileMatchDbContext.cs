using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Persistence
{
    public class ProfileMatchDbContext : DbContext
    {
        public ProfileMatchDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {

        }

        public DbSet<ProfileMatchingResult> Results { get; set; }
    }
}
