using System.ComponentModel;

namespace Somativa.Models
{
    public class VendaItem
    {
        public Guid VendaItemId { get; set; }
        [DisplayName("Nota Fiscal")]
        public Guid VendaId { get; set; }
        public Venda? Venda { get; set; }
        [DisplayName("Produto")]
        public Guid ProdutoId { get; set; }
        public Produto? Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal Unitario { get; set; }
    }
}
