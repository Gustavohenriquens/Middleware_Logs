    using Microsoft.EntityFrameworkCore;
using TesteMiddleware.api.Entities;

namespace TesteMiddleware.api.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<EstoqueM> estoqueMs { get; set; }
        public DbSet<LoginModel> loginMs { get; set; }

        public DbSet<User> userMs { get; set; }


        public async Task ProcessarDadosProtegidos(string userName, User model)
        {

        }
    }
}
