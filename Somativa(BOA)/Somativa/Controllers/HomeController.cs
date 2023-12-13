using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Somativa.Data;
using Somativa.Models;
using System.Diagnostics;

namespace Somativa.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SprintContext _context;

        public HomeController(ILogger<HomeController> logger, SprintContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Produtinhos(Guid Id)
        {
            List<Categoria> categs = await _context.Categorias.ToListAsync();
            ViewData["Categorias"] = categs;


            var sprintContext = _context.Produtos.Include(p => p.Categoria).Include(p => p.Fornecedor);

            if (Id != null && !Id.ToString().Equals("00000000-0000-0000-0000-000000000000"))
            {
                return View(await sprintContext.Where(i => i.CategoriaId == Id).ToListAsync());
            }

            return View(await sprintContext.ToListAsync());
        }

        public IActionResult IndexTables()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}