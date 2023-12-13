using System.ComponentModel;

namespace Somativa.Models
{
    public class Produto
    {
        public Guid ProdutoId { get; set; }
        public string Nome { get; set; }
        public int Estoque { get; set; }
        [DisplayName("Preço")]
        public decimal Preco {  get; set; }
        [DisplayName("Categoria")]
        public Guid CategoriaId { get; set; }
        public Categoria? Categoria { get; set;}
        [DisplayName("Fornecedor")]
        public Guid FornecedorId { get; set; } 
        public Fornecedor? Fornecedor { get; set; }

        public string? Imagem { get; set; }

    }
}
