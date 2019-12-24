using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using GoseiVn.DemoApp.Configuration;
using GoseiVn.DemoApp.Web;

namespace GoseiVn.DemoApp.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class DemoAppDbContextFactory : IDesignTimeDbContextFactory<DemoAppDbContext>
    {
        public DemoAppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DemoAppDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DemoAppDbContextConfigurer.Configure(builder, configuration.GetConnectionString(DemoAppConsts.ConnectionStringName));

            return new DemoAppDbContext(builder.Options);
        }
    }
}
