using Microsoft.AspNetCore.Mvc;
using PaymentApi.Context;
using PaymentApi.Entities;

namespace PaymentApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendedorController: ControllerBase
    {
        private readonly PaymentContext _context;

        public VendedorController(PaymentContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AdicionarVendedor(Vendedor vendedor)
        {
            _context.Add(vendedor);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { id = vendedor.Id }, vendedor);
        }

        [HttpGet("ObterTodosVendedores")]
        public IActionResult ObterTodos()
        {
            var vendedores = _context.Vendedores.ToList();
            return Ok(vendedores);
        }

        [HttpGet("ObterVendedorPorId/{id}")]
        public IActionResult ObterPorId(int id)
        {
            var vendedor = _context.Vendedores.Find(id);
            if (vendedor == null) return NotFound();
            return Ok(vendedor);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Vendedor vendedor)
        {
            var vendedorBanco = _context.Vendedores.Find(id);
            if (id == null) return NotFound();
            vendedorBanco.Nome = vendedor.Nome;
            vendedorBanco.Cpf = vendedor.Cpf;
            vendedorBanco.Nome = vendedor.Nome;
            vendedorBanco.Email = vendedor.Email;
            vendedorBanco.Telefone = vendedor.Telefone;
            _context.Vendedores.Update(vendedorBanco);
            _context.SaveChanges();
            return Ok(vendedorBanco);

        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var vendedorBanco = _context.Vendedores.Find(id);
            if (vendedorBanco == null) return NotFound();
            _context.Vendedores.Remove(vendedorBanco);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
