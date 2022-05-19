using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudImovel.Data;

namespace CrudImovel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoesController : ControllerBase
    {
        private readonly AppDbcontext _context;

        public ProdutoesController(AppDbcontext context)
        {
            _context = context;
        }

        // GET: api/Produtoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Imovel>>> GetProdutos()
        {
            return await _context.Imovel.ToListAsync();
        }

        // GET: api/Produtoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Imovel>> GetProduto(int id)
        {
            var produto = await _context.Imovel.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            return produto;
        }

        // PUT: api/Produtoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, Imovel produto)
        {
            if (id != produto.Id)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Produtoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Imovel>> PostProduto(Imovel produto)
        {
            _context.Imovel.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduto", new { id = produto.Id }, produto);
        }

        // DELETE: api/Produtoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var produto = await _context.Imovel.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            _context.Imovel.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProdutoExists(int id)
        {
            return _context.Imovel.Any(e => e.Id == id);
        }
    }
}
