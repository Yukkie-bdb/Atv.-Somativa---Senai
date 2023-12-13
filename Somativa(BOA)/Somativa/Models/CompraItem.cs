using System.ComponentModel;

namespace Somativa.Models
{
    public class CompraItem
    {
        public Guid CompraItemId { get; set; }
        [DisplayName("Nota Fiscal")]
        public Guid CompraId { get; set; }
        public Compra? Compra { get; set; }
        [DisplayName("Produto")]
        public Guid ProdutoId { get; set; }
        public Produto? Produto { get; set; }
        [DisplayName("Quant.")]

        public int Quantidade { get; set; }
        public decimal Unitario { get; set; }
    }
}
