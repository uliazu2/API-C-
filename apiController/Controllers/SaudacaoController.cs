using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaudacaoController : ControllerBase
    {
        [HttpGet("ola")] // GET /api/saudacao/ola

        //public string GetOla() => "Olá, mundo!";

        public string GetOla()
        {
            return "Olá, mundo!";
        }

        [HttpGet("datahora")] // GET /api/saudacao/datahora
        public string GetDataHora()
        {
            return $"Data e Hora: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
        }

        [HttpGet("{nome}")] // GET /api/saudacao/Andre
        public string GetSaudacaoPorNome(string nome)
        {
            return $"Olá, {nome}! Seja bem-vindo(a)!";
        }
    }
}
