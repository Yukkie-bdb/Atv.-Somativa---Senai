using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using Somativa.Data;
using Somativa.Models;
using System.Security.Policy;

namespace Somativa.Controllers
{
    [Authorize(Roles = "Admin,Gerente")]
    public class RelatorioController : Controller
    {
        private readonly SprintContext _context;

        public RelatorioController(SprintContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> RelatProd()
        {
            var prod = await _context.Produtos.Include(c => c.Categoria).ToListAsync();
            ViewData["Categ"] = await _context.Categorias.ToListAsync();
            
            return View(prod);
        }

        public async Task<IActionResult> SearchProd(Guid inCategoria, string inNome)
        {
			var prod = await _context.Produtos.Include(c => c.Categoria).ToListAsync();

            if (inNome != null) 
            {
				inNome = inNome.Trim().ToUpper();
                prod = prod.Where(i => i.Nome.ToUpper().Contains(inNome)).ToList();
			}

            if (!inCategoria.ToString().Equals("00000000-0000-0000-0000-000000000000"))
            {
                prod = prod.Where(i => i.CategoriaId == inCategoria).ToList();
            }

			ViewData["Categ"] = await _context.Categorias.ToListAsync();
			return View("RelatProd", prod);
        }

		public async Task<IActionResult> RelatMov()
        {
            var mov = await MovimentacaoList.getList(_context);
            ViewData["Produtos"] =  await _context.Produtos.ToListAsync();

			return View(mov);
        }

        public async Task<IActionResult> SearchMov(DateTime? inDataIni, DateTime? inDataFim, int inTipo, string inProduto)
        {
			var mov = await MovimentacaoList.getList(_context);
			ViewData["Produtos"] = await _context.Produtos.ToListAsync();

            if (inDataIni != null) mov = mov.Where(i => i.DataHora >= inDataIni).ToList();
			if (inDataFim != null) mov = mov.Where(i => i.DataHora <= inDataFim).ToList();

            if (inTipo == 1) mov = mov.Where(i => i.TipoMovimentacao.Equals("Entrada")).ToList();
			if (inTipo == 2) mov = mov.Where(i => i.TipoMovimentacao.Equals("Saída")).ToList();

            if (!inProduto.ToString().Equals("Todos"))
                mov = mov.Where(i => i.Produto.Equals(inProduto)).ToList();

			return View("RelatMov", mov);
        }

        public IActionResult GetImage2(string fileName) 
        {
           
            var imgPath = Path.Combine("../MinhasImagens", fileName );
			if (System.IO.File.Exists(imgPath))
            {
				var imageBytes = System.IO.File.ReadAllBytes(imgPath);
				return File(imageBytes, "image/png");
			}
			return NotFound();
		}

		public IActionResult GetImage(string imageName)
		{
			string applicationPath = AppDomain.CurrentDomain.BaseDirectory;
            string teste = System.IO.Directory.GetCurrentDirectory();
			//var imagePath = Path.Combine(applicationPath + "Somativa\\MinhasImagens", imageName);
		
			var imagePath = teste + "\\minhasimagens\\imagem.png";

			if (System.IO.File.Exists(imagePath))
			{
				var imageBytes = System.IO.File.ReadAllBytes(imagePath);
				return File(imageBytes, "image/png");
			}

			return NotFound();
		}



	}
}
