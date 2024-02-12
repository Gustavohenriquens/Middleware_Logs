using Microsoft.AspNetCore.Mvc;
using TesteMiddleware.api.Entities;
using TesteMiddleware.api.Middleware;
using TesteMiddleware.api.Persistence;
using TesteMiddleware.api.Services;
using TesteMiddleware.api.ErrosHTTP;
using Microsoft.AspNetCore.Authorization;
using Serilog;

namespace TesteMiddleware.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstoqueMasculinoController : ControllerBase
    {
        private readonly AppDbContext _db;
        //private readonly ITokenService _tokenService;

        public EstoqueMasculinoController(AppDbContext db/*, ITokenService tokenService*/)
        {
            _db = db;
            //_tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //throw new ArgumentException(); //400
            //throw new UnauthorizedAccessException("Acesso não autorizado."); // 401
            //throw new PaymentRequiredException("Pagamento necessário"); //402
            //throw new ForbiddenException("Acesso proibido"); //403
            throw new NotFoundException("..."); //404
            //throw new ConflictException("Conflito!"); //409
            //throw new InvalidOperationException("Erro no conteúdo ou dados."); //422



            //var exibirTodosOsEstoques = _db.estoqueMs.Where(e => !e.IsDeleted).ToList();
            //// Será uma lista de entidades mEstoqueEntities que não foram marcadas como excluídas (IsDeleted é false).
            //// ToList(): Converte o resultado da consulta em uma lista.
            //return Ok(exibirTodosOsEstoques);
        }


        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var pegarEstoqueApartirDoId = _db.estoqueMs.SingleOrDefault(e => e.IdProduto == id);
            // Está sendo usado para encontrar uma única entidade que satisfaça a condição especificada.
            // É usada para localizar uma entidade com um ID específico no banco de dados.

            if (pegarEstoqueApartirDoId == null)
            {
                //throw new NotFoundException($"Recurso com ID {id} não encontrado.");
                return NotFound(); //Erro de não encontrado.
                //throw new NotFoundException($"Recurso com ID {id} não encontrado.");
            }

            Log.Error("Deu algum erro => {@pegarEstoqueApartirDoId}", pegarEstoqueApartirDoId);

            return Ok(pegarEstoqueApartirDoId);

        }


        [HttpPost]
        public IActionResult Post(EstoqueM mEstoque)
        //MEstoqueEntities é um tipo de dado, e mEstoque é o nome da variável que representa um objeto dessa classe. 
        {
            //_db.mEstoqueEntities.Add(mEstoque); // Essa linha efetivamente adiciona um novo registro ao banco de dados.

            //// Retorna uma resposta de sucesso (código 201 - Created) com detalhes do recurso criado
            //return CreatedAtAction(nameof(Post), new { id = mEstoque.IdProduto }, mEstoque);

            //// Se o modelo não for válido, retorne uma resposta de erro (código 400 - Bad Request)
            ////return BadRequest(ModelState);
            ///
            //throw new Exception();
            throw new InvalidOperationException("Recurso não encontrado.");
        }

















        //[HttpPost]
        //public IActionResult AdicionarUsuario([FromBody]User user)
        //{
        //    try{
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        _db.Add(user);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Erro interno do servidor");
        //    }

        //}

        //[AllowAnonymous]
        //[HttpPost("criarUsuario")]

        //public IActionResult CriarUsuario([FromBody] UserDto criandoUsuarioDto)
        //{

        //    try
        //    {
        //        // Valida se o usuário já existe no banco de dados pelo Login
        //        if (_db.estoqueMs.Any(u => u.NomeProduto.ToLower() == criandoUsuarioDto.Login.ToLower()))
        //        {
        //            return BadRequest(new { Mensagem = "Usuário com o mesmo login já existe." });
        //        }

        //        // Criar usuário como ativo = true e excluído = false
        //        var novoUsuario = new User
        //        {
        //            Nome = criandoUsuarioDto.Nome,
        //            Login = criandoUsuarioDto.Login,
        //            Email = criandoUsuarioDto.Email,
        //            Senha = criandoUsuarioDto.Senha,

        //        };

        //        // Insere um GUID na ChaveVerificacao
        //        novoUsuario.ChaveVerificacao = Guid.NewGuid().ToString();

        //        // Gera um token para o novo usuário e atribui diretamente ao campo LastToken.
        //        //7.
        //        novoUsuario.LastToken = _tokenService.GenerateToken(novoUsuario).ToString();

        //        // Adiciona o novo usuário ao contexto e salvar no banco de dados
        //        _db.Add(novoUsuario);
        //        _db.SaveChanges();

        //        return Ok(new { Mensagem = "Usuário criado com sucesso." });
        //    }
        //    catch(Exception ex)
        //    {
        //        return NotFound(ex.Message);
        //    }

        //}


        //private string HashSenha(string senha)
        //{
        //    using (SHA256 sha256 = SHA256.Create())
        //    {
        //        byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));

        //        // Converte os bytes hash para uma string hexadecimal
        //        StringBuilder builder = new StringBuilder();
        //        for (int i = 0; i < hashedBytes.Length; i++)
        //        {
        //            builder.Append(hashedBytes[i].ToString("x2"));
        //        }

        //        return builder.ToString();
        //    }
        //}


    }
}
