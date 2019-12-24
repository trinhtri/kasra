using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using GoseiVn.DemoApp.Authorization.Roles;
using GoseiVn.DemoApp.Authorization.Users;
using GoseiVn.DemoApp.MultiTenancy;
using GoseiVn.DemoApp.Models;
using GoseiVn.DemoApp.Configurations;

namespace GoseiVn.DemoApp.EntityFrameworkCore
{
    public class DemoAppDbContext : AbpZeroDbContext<Tenant, Role, User, DemoAppDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public virtual DbSet<Estimates> Estimates { get; set; }
        public virtual DbSet<States> States { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public DemoAppDbContext(DbContextOptions<DemoAppDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EstimateDbConfig.Configure(modelBuilder.Entity<Estimates>());
            StatesDbConfig.Configure(modelBuilder.Entity<States>());
            ImagesDbConfig.Configure(modelBuilder.Entity<Images>());
        }
    }
}
