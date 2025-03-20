using Microsoft.EntityFrameworkCore;
using DentinhoFeliz.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace DentinhoFeliz.Infrastructure
{
    public class DentinhoFelizContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DentinhoFelizContext(DbContextOptions<DentinhoFelizContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Alarme> Alarmes { get; set; }
        public DbSet<Duvida> Duvidas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                    ?? _configuration.GetConnectionString("DefaultConnection");

                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("String de conexão não encontrada!");
                }

                optionsBuilder.UseOracle(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração das entidades no banco de dados Oracle
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("USUARIO");
                entity.Property(e => e.Id).HasColumnName("ID").HasColumnType("NUMBER(10)");
                entity.Property(e => e.Nome).HasColumnName("NOME").HasColumnType("NVARCHAR2(2000)");
                entity.Property(e => e.Email).HasColumnName("EMAIL").HasColumnType("NVARCHAR2(2000)");
                entity.Property(e => e.Senha).HasColumnName("SENHA").HasColumnType("NVARCHAR2(2000)");
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.ToTable("QUIZ");
                entity.Property(e => e.Id).HasColumnName("ID").HasColumnType("NUMBER(10)");
                entity.Property(e => e.Pergunta).HasColumnName("PERGUNTA").HasColumnType("NVARCHAR2(2000)");
                entity.Property(e => e.OpcoesString).HasColumnName("OPCOES").HasColumnType("NVARCHAR2(2000)");
                entity.Property(e => e.Resposta).HasColumnName("RESPOSTA").HasColumnType("NVARCHAR2(2000)");
            });

            modelBuilder.Entity<Alarme>(entity =>
            {
                entity.ToTable("ALARME");
                entity.Property(e => e.Id).HasColumnName("ID").HasColumnType("NUMBER(10)");
                entity.Property(e => e.Horario).HasColumnName("HORARIO").HasColumnType("TIMESTAMP(7)");
                entity.Property(e => e.Mensagem).HasColumnName("MENSAGEM").HasColumnType("NVARCHAR2(2000)");
            });

            modelBuilder.Entity<Duvida>(entity =>
            {
                entity.ToTable("DUVIDA");
                entity.Property(e => e.Id).HasColumnName("ID").HasColumnType("NUMBER(10)");
                entity.Property(e => e.Pergunta).HasColumnName("PERGUNTA").HasColumnType("NVARCHAR2(2000)");
                entity.Property(e => e.Resposta).HasColumnName("RESPOSTA").HasColumnType("NVARCHAR2(2000)");
            });
        }
    }
}