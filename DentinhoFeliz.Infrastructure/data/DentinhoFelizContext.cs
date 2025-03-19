using Microsoft.EntityFrameworkCore;
using DentinhoFeliz.Domain.Entities;

namespace DentinhoFeliz.Infrastructure
{
    public class DentinhoFelizContext : DbContext
    {
        public DentinhoFelizContext(DbContextOptions<DentinhoFelizContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Alarme> Alarmes { get; set; }
        public DbSet<Duvida> Duvidas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseOracle("User Id=rm553034;Password=081099;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=orcl.fiap.com.br)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=ORCL)))");
            }
        }
    }
}