using apiController.Models;
using Microsoft.AspNetCore.Mvc;
// (NOVO) usando o DbContext
using apiController.Data;
using apiController.DTO;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class TarefasController : ControllerBase
{
    // ======================================
    // LISTA ESTÁTICA (REMOVER)
    // private static List<Tarefa> _tarefas = new()
    // {
    //     new Tarefa { Id = 1, Titulo = "Estudar ASP.NET Core", Concluida = false },
    //     new Tarefa { Id = 2, Titulo = "Preparar apresentação", Concluida = false },
    //     new Tarefa { Id = 3, Titulo = "Fazer compras", Concluida = true }
    // };
    // ======================================

    // Campo privado e somente leitura que vai guardar a instância do AppDbContext.
    // Esse campo é usado pelos métodos do controller para consultar e salvar dados no banco.
    private readonly AppDbContext _db;

    // Construtor do controller.
    // O ASP.NET Core, através da Injeção de Dependência (DI), fornece automaticamente
    // uma instância de AppDbContext já configurada em Program.cs (com provider e connection string).
    // Isso evita que você precise usar "new AppDbContext(...)" manualmente.
    public TarefasController(AppDbContext db)
    {
        // A instância recebida pelo construtor é atribuída ao campo privado "_db",
        // para que todos os endpoints deste controller possam utilizá-la.
        _db = db;
    }
    // GET /api/tarefas
    [HttpGet]
    public IActionResult GetTodas() {

        var lista = _db.Tarefas.AsNoTracking().Select(t => new ExibirTarefaDTO
        {
            Id = t.Id,
            Titulo = t.Titulo,
            Concluida = t.Concluida,
        }).ToList();
        // ANTES: return _tarefas;
        // NOVO: consulta no banco, sem tracking (mais leve para leitura)
        return Ok(lista);
    }

    // GET /api/tarefas/{id}
    [HttpGet("{id}")]
    public IActionResult GetPorId(int id)
    //public Tarefa? GetPorId(int id)
    {
        // ANTES: buscava na lista estática
        // var tarefaEncontrada = _tarefas.FirstOrDefault(t => t.Id == id);
        
        // NOVO: busca no banco
        var tarefa = _db.Tarefas.FirstOrDefault(t => t.Id == id);
        if (tarefa is null)
        {
            return NotFound();
        }

        var dto = new ExibirTarefaDTO
        {
            Id = tarefa.Id,
            Titulo = tarefa.Titulo,
            Concluida = tarefa.Concluida
        };
        return Ok(dto);
    }
    
    // POST /api/tarefas
    [HttpPost]
    public IActionResult Adicionar(CriarTarefaDto dto)
    //public Tarefa Adicionar(Tarefa novaTarefa)
    {
        var entity = new Tarefa
        {
            Titulo = dto.Titulo,
            Concluida = dto.Concluida,
            DateDeCriacao = DateTime.Now
        };
        // ANTES: calcular manualmente o Id e adicionar na lista
        // int novoId = _tarefas.Any() ? _tarefas.Max(t => t.Id) + 1 : 1;
        // novaTarefa.Id = novoId;
        // _tarefas.Add(novaTarefa);

        // NOVO: o banco gera o Id automaticamente

        _db.Tarefas.Add(entity);
        _db.SaveChanges(); // grava no SQLite
        var saida = new ExibirTarefaDTO
        {
            Id = entity.Id,
            Titulo = entity.Titulo,
            Concluida = entity.Concluida
        };
        return CreatedAtAction(
            nameof(GetPorId),
            new {id = entity.Id},
            saida
        );
    }

    // PUT /api/tarefas/{id}
    [HttpPut("{id}")]
    public IActionResult Atualizar(int id, CriarTarefaDto dto)
    {
        var existente = _db.Tarefas.FirstOrDefault(t => t.Id == id);
        if (existente == null)
        {
            return NotFound();
        }
        existente.Titulo = dto.Titulo;
        existente.Concluida = dto.Concluida;

        _db.SaveChanges();
        var exibirDTO = new ExibirTarefaDTO
        {
            Id = existente.Id,
            Titulo = existente.Titulo,
            Concluida = existente.Concluida
        };

        return Ok(exibirDTO);
    }



    // DELETE /api/tarefas/{id}
    [HttpDelete("{id}")]
    public IActionResult Remover(int id)
    {

        var existente = _db.Tarefas.FirstOrDefault(t => t.Id == id);
        if (existente == null)
        {
            return NotFound();
        }
        var dto = new ExibirTarefaDTO
        {
            Id = existente.Id,
            Titulo = existente.Titulo,
            Concluida = existente.Concluida
        };
        _db.Tarefas.Remove(existente);
        _db.SaveChanges();

        
        return Ok(dto);
    }


    // GET /api/tarefas/concluidas
    [HttpGet("concluidas")]
    //public ActionResult<Tarefa> Concluidas()
    public IEnumerable<ExibirTarefaDTO> GetConcluidas()
    {
        var tarefasConcluidas = _db.Tarefas
                                   .AsNoTracking()
                                   .Where(t => t.Concluida)
                                   .ToList();
        var tarefasDTO = tarefasConcluidas.Select(t => new ExibirTarefaDTO
        {
            Id = t.Id,
            Titulo = t.Titulo,
            Concluida = t.Concluida
        });
        return tarefasDTO;
    }

}
