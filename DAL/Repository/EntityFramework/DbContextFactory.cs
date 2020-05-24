using System.Configuration;

namespace DAL.Repository.EntityFramework
{
    public static class DbContextFactory
    {
        public static EfDbContext CreateEfDbContext(string connectionStringName = "AuthorizePracticeByRole")
        {
            var setting = ConfigurationManager.ConnectionStrings[connectionStringName];
            if (setting == null)
            {
                return null;
            }

            var result = new EfDbContext();
            result.Database.Connection.ConnectionString   = setting.ConnectionString;
            result.Configuration.AutoDetectChangesEnabled = false;
            result.Configuration.LazyLoadingEnabled       = false;
            result.Configuration.ProxyCreationEnabled     = false;
            return result;
        }
    }
}