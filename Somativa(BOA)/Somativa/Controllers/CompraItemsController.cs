using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Somativa.Data;
using Somativa.Models;

namespace Somativa.Controllers
{
	[Authorize(Roles = "Admin,Operador")]
	public class CompraItemsController : Controller
    {
        private readonly SprintContext _context;

        public CompraItemsController(SprintContext context)
        {
            _context = context;
        }

        // GET: CompraItems
        public async Task<IActionResult> Index(Guid Id)
        {
            var sprintContext = _context.CompraItens.Include(c => c.Compra).Include(c => c.Produto);
            ViewData["CompraId"] = Id;
            return View(await sprintContext.Where(i => i.CompraId == Id).ToListAsync());
        }

        // GET: CompraItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.CompraItens == null)
            {
                return NotFound();
            }

            var compraItem = await _context.CompraItens
                .Include(c => c.Compra)
                .Include(c => c.Produto)
                .FirstOrDefaultAsync(m => m.CompraItemId == id);
            if (compraItem == null)
            {
                return NotFound();
            }

            return View(compraItem);
        }

        // GET: CompraItems/Create
        public IActionResult Create(Guid Id)
        {
            //ViewData["CompraId"] = new SelectList(_context.Compras, "CompraId", "CompraId");            
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Nome");
            ViewData["CompraId"] = Id;
            return View();
        }

        // POST: CompraItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompraItemId,CompraId,ProdutoId,Quantidade,Unitario")] CompraItem compraItem)
        {
            if (ModelState.IsValid)
            {
                compraItem.CompraItemId = Guid.NewGuid();
                _context.Add(compraItem);
                await _context.SaveChangesAsync();

                Produto p = _context.Produtos.Where(p => p.ProdutoId == compraItem.ProdutoId).FirstOrDefault();
                p.Estoque += compraItem.Quantidade;
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { Id = compraItem.CompraId });
            }
            ViewData["CompraId"] = new SelectList(_context.Compras, "CompraId", "Nota", compraItem.CompraId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Nome", compraItem.ProdutoId);
            return View(compraItem);
        }

        // GET: CompraItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.CompraItens == null)
            {
                return NotFound();
            }

            var compraItem = await _context.CompraItens.FindAsync(id);
            if (compraItem == null)
            {
                return NotFound();
            }
            ViewData["CompraId"] = new SelectList(_context.Compras, "CompraId", "Nota", compraItem.CompraId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Nome", compraItem.ProdutoId);
            return View(compraItem);
        }

        // POST: CompraItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CompraItemId,CompraId,ProdutoId,Quantidade,Unitario")] CompraItem compraItem)
        {
            if (id != compraItem.CompraItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compraItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraItemExists(compraItem.CompraItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompraId"] = new SelectList(_context.Compras, "CompraId", "Nota", compraItem.CompraId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Nome", compraItem.ProdutoId);
            return View(compraItem);
        }

        // GET: CompraItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.CompraItens == null)
            {
                return NotFound();
            }

            var compraItem = await _context.CompraItens
                .Include(c => c.Compra)
                .Include(c => c.Produto)
                .FirstOrDefaultAsync(m => m.CompraItemId == id);
            if (compraItem == null)
            {
                return NotFound();
            }

            return View(compraItem);
        }

        // POST: CompraItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.CompraItens == null)
            {
                return Problem("Entity set 'SprintContext.CompraItens'  is null.");
            }
            var compraItem = await _context.CompraItens.FindAsync(id);
            if (compraItem != null)
            {
                _context.CompraItens.Remove(compraItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompraItemExists(Guid id)
        {
          return (_context.CompraItens?.Any(e => e.CompraItemId == id)).GetValueOrDefault();
        }
    }
}
