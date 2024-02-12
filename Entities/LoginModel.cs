using System.ComponentModel.DataAnnotations;

namespace TesteMiddleware.api.Entities
{
    public class LoginModel
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
