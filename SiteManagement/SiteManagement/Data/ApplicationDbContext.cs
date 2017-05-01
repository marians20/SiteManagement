using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiteManagement.Models;

namespace SiteManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<SiteManagement.Models.Person> People { get; set; }
        public DbSet<SiteManagement.Models.MeasureUnit> MeasureUnits { get; set; }
        public DbSet<SiteManagement.Models.Group> Groups { get; set; }
        public DbSet<SiteManagement.Models.Lv> Lvs { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public void DeleteAll<T>()
        where T : class
        {
            foreach (var p in Set<T>())
            {
                Entry(p).State = EntityState.Deleted;
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Person>().HasIndex(x => x.Mark).IsUnique(true);
            builder.Entity<Person>().Property(x => x.FirstName).HasMaxLength(64);
            builder.Entity<Person>().Property(x => x.LastName).HasMaxLength(64);
            builder.Entity<Person>().Property(x => x.Mark).HasMaxLength(32);

            builder.Entity<MeasureUnit>().HasIndex(x => x.Name);
            builder.Entity<MeasureUnit>().Property(x => x.Name).HasMaxLength(32);

            builder.Entity<Group>().HasIndex(x => new { x.Name, x.Number }).IsUnique(true);
            builder.Entity<Group>().Property(x => x.Name).HasMaxLength(128);
        }
    }
}
