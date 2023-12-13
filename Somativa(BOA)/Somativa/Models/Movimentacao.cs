using System.ComponentModel;
using System.Security.Policy;

namespace Somativa.Models
{
	public class Movimentacao
	{		
		public Guid Id { get; set; }
		[DisplayName("Nota Fiscal")]
		public int Nota { get; set; }
		[DisplayName("Data e hora")]
		public DateTime DataHora { get; set; }
		[DisplayName("Tipo de movimentação")]
		public string TipoMovimentacao { get; set; }
		public string Produto { get; set; }		
		public int Quantidade { get; set; }
		[DisplayName("Preço unitário")]
		public decimal Unitario { get; set; }
	}
}
