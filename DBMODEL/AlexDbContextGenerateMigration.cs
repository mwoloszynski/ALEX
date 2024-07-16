using Microsoft.EntityFrameworkCore.Design;

namespace DBMODEL
{
    internal class AlexDbContextGenerateMigration : IDesignTimeDbContextFactory<AlexDbContext>
    {
        public AlexDbContext CreateDbContext(string[] args)
        {
            return new AlexDbContext(args[0]);
        }
    }
}
