using System.ComponentModel;

namespace Somativa.Models
{
    public class Venda
    {
        public Guid VendaId { get; set; }
        [DisplayName("Nota Fiscal")]
        public int Nota { get; set; }
        [DisplayName("Cliente")]
        public Guid ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        [DisplayName("Data e hora")]
        public DateTime DataHora { get; set; }
        public IEnumerable<VendaItem>? VendaItems { get; set; }
    }
}
