using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using GoseiVn.DemoApp.Authorization.Roles;
using GoseiVn.DemoApp.Authorization.Users;
using GoseiVn.DemoApp.MultiTenancy;

namespace GoseiVn.DemoApp.EntityFrameworkCore
{
    public class DemoAppDbContext : AbpZeroDbContext<Tenant, Role, User, DemoAppDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public DemoAppDbContext(DbContextOptions<DemoAppDbContext> options)
            : base(options)
        {
        }
    }
}
