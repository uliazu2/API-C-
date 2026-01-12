using System.ComponentModel.DataAnnotations;
namespace apiController.DTO
{
    public class AtualizarTarefaDto
    {
        //dto protege as entidades
        [Required(ErrorMessage = "O campo título é obrigatório.")]
        [StringLength(120, MinimumLength = 3, ErrorMessage = "O campo título precisa ter entre 3 e 120 caracteres.")]
        public string Titulo { get; set; } = "";
        public bool Concluida { get; set; }
    }
}
