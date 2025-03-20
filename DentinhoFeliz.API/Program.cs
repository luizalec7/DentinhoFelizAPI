using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using DentinhoFeliz.Infrastructure;
using System;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Configuração do banco de dados Oracle (Apontando para `dentinhofelizdbdotnet`)
builder.Services.AddDbContext<DentinhoFelizContext>(options =>
    options.UseOracle("User Id=seu_usuario;Password=sua_senha;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=dentinhofelizdbdotnet)))"));

// 🔹 Adicionando serviços necessários
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Dentinho Feliz API",
        Version = "v1",
        Description = "API para gerenciamento de usuários, quizzes, alarmes e dúvidas no aplicativo Dentinho Feliz",
        Contact = new OpenApiContact
        {
            Name = "Suporte Dentinho Feliz",
            Email = "suporte@dentinhofeliz.com",
            Url = new Uri("https://dentinhofeliz.com")
        }
    });
});

var app = builder.Build();

// 🔹 Criar as tabelas automaticamente no Oracle (Migrations Automáticas)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DentinhoFelizContext>();

    try
    {
        context.Database.Migrate(); // Aplica todas as migrações pendentes
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Erro ao aplicar migrações: {ex.Message}");
    }
}

// 🔹 Configuração do Swagger para testes de API
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dentinho Feliz API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization(); // 🔥 Removido `UseAuthentication()`, pois não há autenticação JWT
app.MapControllers();

app.Run();