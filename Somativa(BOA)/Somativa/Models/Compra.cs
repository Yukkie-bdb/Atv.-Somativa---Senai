using System.ComponentModel;

namespace Somativa.Models
{
    public class Compra
    {
        public Guid CompraId { get; set; }
        [DisplayName("Nota Fiscal")]
        public int Nota { get; set; }
        [DisplayName("Data e hora")]
        public DateTime DataHora { get; set; }
        public IEnumerable<CompraItem>? CompraItems { get; set; }
    }
}
