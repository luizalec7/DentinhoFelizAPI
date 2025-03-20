using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace DentinhoFeliz.Infrastructure
{
    public class DentinhoFelizContextFactory : IDesignTimeDbContextFactory<DentinhoFelizContext>
    {
        public DentinhoFelizContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();
            var appSettingsPath = Path.Combine(basePath, "appsettings.json");

            if (!File.Exists(appSettingsPath))
            {
                throw new FileNotFoundException("Arquivo appsettings.json não encontrado!", appSettingsPath);
            }

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine($"🔍 Connection String Carregada: {connectionString}");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada no appsettings.json.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<DentinhoFelizContext>();
            optionsBuilder.UseOracle(connectionString);

            // Testa conexão ANTES de retornar o DbContext
            using (var connection = new Oracle.ManagedDataAccess.Client.OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("✅ Conexão bem-sucedida!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Erro ao conectar ao banco: {ex.Message}");
                }
            }

            return new DentinhoFelizContext(optionsBuilder.Options, configuration);
        }

    }
}