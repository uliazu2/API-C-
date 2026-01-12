using Microsoft.EntityFrameworkCore;        //Acrescentar
using apiController.Models;                 //Acrescentar

namespace apiController.Data
{
    public class AppDbContext : DbContext
    {
        // Este construtor é chamado PELO ASP.NET Core,
        // que fornece automaticamente um objeto DbContextOptions<AppDbContext>.
        // Esse objeto contém a configuração definida no Program.cs (provider, connection string, etc).
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Mapeia a entidade Tarefa para a tabela "Tarefas"
        // Representa a tabela "Tarefas" no banco.
        // Consultas e alterações são feitas por aqui.
        public DbSet<Tarefa> Tarefas => Set<Tarefa>();
    }
}
