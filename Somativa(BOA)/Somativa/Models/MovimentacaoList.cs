using Microsoft.EntityFrameworkCore;
using Somativa.Data;

namespace Somativa.Models
{
	public static class MovimentacaoList
	{
		private static List<Movimentacao> lista;

		public static async Task<List<Movimentacao>> getList(SprintContext _context)
		{
			if (lista == null) { lista = new List<Movimentacao>(); }

			lista.Clear();

			var vendas = await _context.VendaItens.Include(v => v.Venda).Include(p => p.Produto).ToListAsync();
			var compras = await _context.CompraItens.Include(c => c.Compra).Include(p => p.Produto).ToListAsync();

			foreach ( var v in vendas ) 
				lista.Add(
					new Movimentacao
					{
						Id = v.VendaItemId,
						Nota = v.Venda.Nota,
						DataHora = v.Venda.DataHora,
						Produto = v.Produto.Nome,
						Quantidade = v.Quantidade,
						Unitario = v.Unitario,
						TipoMovimentacao = "Saída"
					}
				);

			foreach (var c in compras)
				lista.Add(
					new Movimentacao
					{
						Id = c.CompraItemId,
						Nota = c.Compra.Nota,
						DataHora = c.Compra.DataHora,
						Produto = c.Produto.Nome,
						Quantidade = c.Quantidade,
						Unitario = c.Unitario,
						TipoMovimentacao = "Entrada"
					}
				);

			return lista.OrderBy(d => d.DataHora).ToList();
		}
	}
}
