namespace apiController.Models
{
    // Models/Tarefa.cs
    // Representa os dados que iremos persistir.
    // EF Core mapeia propriedades públicas para colunas automaticamente (convenções).
    public class Tarefa
    {
        // Por convenção, "Id" vira PK com valor gerado pelo banco.
        public int Id { get; set; }

        // Título da tarefa; inicializamos para evitar null reference.
        public string Titulo { get; set; } = "";

        // True se a tarefa foi concluída.
        public bool Concluida { get; set; }

        public DateTime DateDeCriacao { get; set; }

        public string Descricao { get; set; }
    }
    
}
