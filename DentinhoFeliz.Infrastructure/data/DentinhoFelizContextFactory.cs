using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DentinhoFeliz.Infrastructure
{
    public class DentinhoFelizContextFactory : IDesignTimeDbContextFactory<DentinhoFelizContext>
    {
        public DentinhoFelizContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../DentinhoFeliz.API");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath) // Caminho correto do appsettings.json
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DentinhoFelizContext>();
            optionsBuilder.UseOracle(configuration.GetConnectionString("DefaultConnection"));

            return new DentinhoFelizContext(optionsBuilder.Options);
        }
    }
}