// Models/DTOs/CriarTarefaDto.cs
using System.ComponentModel.DataAnnotations;

// 1. O DTO agora implementa a interface
public class CriarTarefaDto : IValidatableObject
{
    [Required(ErrorMessage = "O campo Título é obrigatório.")]
    [StringLength(120, MinimumLength = 3, ErrorMessage = "O Título deve ter entre 3 e 120 caracteres.")]
    public string Titulo { get; set; } = "";

    // A Descrição é opcional por padrão
    [StringLength(500)]
    public string? Descricao { get; set; }

    public bool Concluida { get; set; }

    // 2. Implementação do método da interface
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        // --- REGRA 1: Título não pode ser igual à Descrição ---
        // (Só executamos se o Título não for nulo/vazio)
        if (!string.IsNullOrWhiteSpace(Titulo) && Titulo.Equals(Descricao, StringComparison.OrdinalIgnoreCase))
        {
            // Retorna um erro que pode ser associado a um ou mais campos
            yield return new ValidationResult(
                "A Descrição não pode ser igual ao Título.",
                // Informa quais campos estão envolvidos no erro
                new[] { nameof(Titulo), nameof(Descricao) }
            );
        }

        // --- REGRA 2: Se o Título for 'Urgente', Descrição é obrigatória ---
        // (Aqui a validação de [Required] no Título já nos ajuda)
        if (!string.IsNullOrWhiteSpace(Titulo) &&
            Titulo.Contains("Urgente", StringComparison.OrdinalIgnoreCase) &&
            string.IsNullOrWhiteSpace(Descricao))
        {
            yield return new ValidationResult(
                "Tarefas marcadas como 'Urgente' no Título exigem uma Descrição.",
                // O erro está na Descricao (que está vazia)
                new[] { nameof(Descricao) }
            );
        }
    }
}
