namespace apiController.DTO
{
    public class ExibirTarefaDTO
    {
        public int Id { get; internal set; }
        public string Titulo { get; set; } = "";
        public bool Concluida { get; set; }
    }
}
