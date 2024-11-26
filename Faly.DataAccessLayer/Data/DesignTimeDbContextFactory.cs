using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Faly.DataAccessLayer.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Faly.Api'nin appsettings.json dosyasının yolunu belirleyin
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Faly.Api")) // Faly.Api dizinine gidiyoruz
                .AddJsonFile("appsettings.json", optional: false) // appsettings.json dosyasını yükleyin
                .Build();

            var connectionString = configuration.GetConnectionString("PostgreSql");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Connection string 'DefaultConnection' not found.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(connectionString); // PostgreSQL kullanımı

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
