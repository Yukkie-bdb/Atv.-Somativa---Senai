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
	public class VendaItemsController : Controller
    {
        private readonly SprintContext _context;

        public VendaItemsController(SprintContext context)
        {
            _context = context;
        }

        // GET: VendaItems
        public async Task<IActionResult> Index(Guid Id)
        {
            var sprintContext = _context.VendaItens.Include(v => v.Produto).Include(v => v.Venda);
            ViewData["VendaId"] =  Id ;
            return View(await sprintContext.Where(i => i.VendaId==Id).ToListAsync());
        }

        // GET: VendaItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.VendaItens == null)
            {
                return NotFound();
            }

            var vendaItem = await _context.VendaItens
                .Include(v => v.Produto)
                .Include(v => v.Venda)
                .FirstOrDefaultAsync(m => m.VendaItemId == id);
            if (vendaItem == null)
            {
                return NotFound();
            }

            return View(vendaItem);
        }

        // GET: VendaItems/Create
        public IActionResult Create(Guid Id)
        {
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Nome");
            
            //ViewData["VendaId"] = new SelectList(_context.Vendas, "VendaId", "Nome");
            ViewData["VendaId"] = Id;          
            return View();
        }

        // POST: VendaItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VendaItemId,VendaId,ProdutoId,Quantidade,Unitario")] VendaItem vendaItem)
        {
            if (ModelState.IsValid)
            {
                vendaItem.VendaItemId = Guid.NewGuid();
                _context.Add(vendaItem);
                await _context.SaveChangesAsync();

                Produto p = _context.Produtos.Where(p => p.ProdutoId == vendaItem.ProdutoId).FirstOrDefault();
                p.Estoque -= vendaItem.Quantidade;
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new {Id = vendaItem.VendaId});
            }
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Produto", vendaItem.ProdutoId);
            ViewData["VendaId"] = new SelectList(_context.Vendas, "VendaId", "Nota", vendaItem.VendaId);
                      

            return View(vendaItem);
        }

        // GET: VendaItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.VendaItens == null)
            {
                return NotFound();
            }

            var vendaItem = await _context.VendaItens.FindAsync(id);
            if (vendaItem == null)
            {
                return NotFound();
            }
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Nome");
            //ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Produto", vendaItem.ProdutoId);
            ViewData["VendaId"] = new SelectList(_context.Vendas, "VendaId", "Nota", vendaItem.VendaId);
            return View(vendaItem);
        }

        // POST: VendaItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("VendaItemId,VendaId,ProdutoId,Quantidade,Unitario")] VendaItem vendaItem)
        {
            if (id != vendaItem.VendaItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendaItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendaItemExists(vendaItem.VendaItemId))
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
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "ProdutoId", "Produto", vendaItem.ProdutoId);
            ViewData["VendaId"] = new SelectList(_context.Vendas, "VendaId", "Nota", vendaItem.VendaId);
            return View(vendaItem);
        }

        // GET: VendaItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.VendaItens == null)
            {
                return NotFound();
            }

            var vendaItem = await _context.VendaItens
                .Include(v => v.Produto)
                .Include(v => v.Venda)
                .FirstOrDefaultAsync(m => m.VendaItemId == id);
            if (vendaItem == null)
            {
                return NotFound();
            }

            return View(vendaItem);
        }

        // POST: VendaItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.VendaItens == null)
            {
                return Problem("Entity set 'SprintContext.VendaItens'  is null.");
            }
            var vendaItem = await _context.VendaItens.FindAsync(id);
            if (vendaItem != null)
            {
                _context.VendaItens.Remove(vendaItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendaItemExists(Guid id)
        {
          return (_context.VendaItens?.Any(e => e.VendaItemId == id)).GetValueOrDefault();
        }
    }
}
