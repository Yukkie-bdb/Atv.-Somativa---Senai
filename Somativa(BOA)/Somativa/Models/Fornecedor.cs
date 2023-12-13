using System.ComponentModel;

namespace Somativa.Models
{
    public class Fornecedor
    {        
        public Guid FornecedorId { get; set; }
        public string Nome { get; set; }
        [DisplayName("CNPJ")]
        public string Cnpj { get; set; }
        public IEnumerable<Produto>? Produtos { get; set; }
    }
}
