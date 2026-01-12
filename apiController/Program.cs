using apiController.Data;
using apiController.Models;         // usado no seed opcional
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Aqui registramos UMA VEZ como criar e configurar o AppDbContext.
// O container de DI guarda essa informação.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//builder.Services → é o contêiner de serviços (DI).

//AddDbContext<AppDbContext> → registra o contexto no DI.

//options.UseSqlite(...) → define que será usado SQLite e pega a string de conexão do appsettings.json.

//Scoped → é o ciclo de vida padrão do AddDbContext, garantindo um DbContext novo para cada requisição HTTP.

// Objetivo deste bloco:
// Garantir que o banco de dados esteja pronto quando a aplicação iniciar,
// criando-o se não existir e aplicando automaticamente as migrations pendentes.

using (var scope = app.Services.CreateScope())
{
    // Criamos um "escopo de serviço" manualmente.
    // Isso é necessário porque o AppDbContext é um serviço *Scoped*,
    // e só pode ser usado dentro de um escopo (normalmente uma requisição HTTP).
    // Aqui no startup não existe requisição, então abrimos esse escopo temporário.

    // "Resolver o serviço" significa pedir ao container de Injeção de Dependência (DI)
    // para entregar uma instância já configurada do AppDbContext.
    // O container sabe como criar porque registramos isso em Program.cs.
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Finalmente, o EF Core garante que:
    // - Crie o banco se não existir
    // - Aplique todas as migrations para deixar o esquema atualizado
    db.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
