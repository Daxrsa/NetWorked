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
         public DbSet<Komenti> Komentet { get; set; }
         public DbSet<Artikulli> Artikujt { get; set; }
        public DbSet<Profesori> Profesoret { get; set; }
        public DbSet<Studenti> Studentet { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Footballer> Footballers { get; set; }
        public DbSet<Specializimi> Specializimet { get; set; }
        public DbSet<Semundja> Semundjet { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Komenti>()
                .HasOne(k => k.Artikulli)
                .WithMany(a => a.Comments)
                .HasForeignKey(k => k.ArticleId);

            modelBuilder.Entity<Profesori>()
                .HasMany(s => s.Students)
                .WithMany(p => p.Profesors);
        
            modelBuilder.Entity<Studenti>()
                .HasMany(p => p.Profesors)
                .WithMany(a => a.Students);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Footballer>()
                .HasOne(k => k.Team)
                .WithMany(a => a.Footballers)
                .HasForeignKey(k => k.TeamId);

            modelBuilder.Entity<Semundja>()
                .HasOne(k => k.Specializimi)
                .WithMany(a => a.Semundjet)
                .HasForeignKey(k => k.SpecializimiId);
        }
    }
}
