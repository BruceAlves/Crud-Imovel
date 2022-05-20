using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudImovel.Data;
using System.Net.Http;
using Newtonsoft.Json;

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
        public async Task<ActionResult<Imovel>> PostProduto(string cep, string tipoImovel, double valorAluguel)
        {
           Imovel imovel = new Imovel();

            HttpClient cliente = new HttpClient();
            HttpResponseMessage resposta = await cliente.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
            var jsonString = resposta.Content.ReadAsStringAsync().Result;
            cepResponce respostaViaCep = JsonConvert.DeserializeObject<cepResponce>(jsonString);

            imovel.Cep = cep;
            imovel.Cidade = respostaViaCep.Localidade;
            imovel.Estado = respostaViaCep.Uf;
            imovel.Rua = respostaViaCep.Logradouro;
            imovel.TipoImovel = tipoImovel;
            imovel.ValorAluguel = valorAluguel;

            _context.Imovel.Add(imovel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduto", new { id = imovel.Id }, imovel);
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
