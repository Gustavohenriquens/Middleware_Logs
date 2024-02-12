using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TesteMiddleware.api.Entities;
using TesteMiddleware.api.ErrosHTTP;
using TesteMiddleware.api.HttpService;
using TesteMiddleware.api.Persistence;

namespace TesteMiddleware.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TesteController : ControllerBase
    {
        private readonly AppDbContext _db;

        public TesteController(AppDbContext db)
        {
            _db = db;
        }

        [Authorize]
        [HttpPost("processar")]
        public async Task<IActionResult> Processar([FromBody] User model)
        {
            // Exemplo de chamada ao método ProcessarDadosProtegidos
            await _db.ProcessarDadosProtegidos(User.Identity.Name, model);
            return Ok();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] User model)
        {
            // Exemplo de ação protegida por autenticação
            _db.ProcessarDadosProtegidos(User.Identity.Name, model);
            return Ok();
        }

    }
}
