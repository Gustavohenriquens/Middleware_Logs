using System.ComponentModel.DataAnnotations;

namespace TesteMiddleware.api.Entities
{
    public class EstoqueM
    {
        public EstoqueM()
        {
            IsDeleted = false;
        }

        [Key]
        public Guid IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public int QuantidadeProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public bool IsDeleted { get; set; } = false;


        public void Update(string nomeProduto, int quantidadeProduto, string descricaoProduto)
        {
            NomeProduto = nomeProduto;
            QuantidadeProduto = quantidadeProduto;
            DescricaoProduto = descricaoProduto;
        }

        public void Delete()
        {
            IsDeleted = true;
        }
    }
}
