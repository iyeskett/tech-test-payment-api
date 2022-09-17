﻿using Microsoft.AspNetCore.Mvc;
using PaymentApi.Context;
using PaymentApi.Entities;

namespace PaymentApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendaController : ControllerBase
    {
        private readonly PaymentContext _context;
        private Vendedor vendedor;

        public VendaController(PaymentContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AdicionarVenda(Venda venda)
        {
            var vendedor = _context.Vendedores.Find(venda.IdVendedor);
            if (vendedor == null) return NotFound("Vendedor não encontrado");
            if (string.IsNullOrEmpty(venda.Itens)) return UnprocessableEntity("Venda sem item");
            venda.StatusVenda = "Aguardando Pagamento";
            _context.Vendas.Add(venda);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { id = venda.Id }, venda);
        }

        [HttpGet]
        public IActionResult ObterTodasVendas()
        {
            var vendas = _context.Vendas.ToList();
            return Ok(vendas);
        }

        [HttpGet("ObterVendaPorId/{id}")]
        public IActionResult ObterPorId(int id)
        {
            var venda = _context.Vendas.Find(id);
            if (venda == null) return NotFound();
            return Ok(venda);
        }

        [HttpPut("AtualizarStatus/{id}")]
        public IActionResult Atualizar(int id, Venda venda)
        {
            var vendaBanco = _context.Vendas.Find(id);
            if (venda == null) return NotFound();
            if (ValidadorAtualizacao.PodeAtualizar(venda, vendaBanco))
            {
                vendaBanco.StatusVenda = venda.StatusVenda;
                _context.SaveChanges();
                return Ok(vendaBanco);
            }
            if (vendaBanco.StatusVenda == "Entregue") return UnprocessableEntity("A venda já foi entregue");
            if (vendaBanco.StatusVenda == "Cancelada") return UnprocessableEntity("A venda foi cancelada");
            return UnprocessableEntity("Status não permitido");
        }
    }

    public static class ValidadorAtualizacao
    {
        public static bool PodeAtualizar(Venda venda, Venda vendaBanco)
        {
            if (vendaBanco.StatusVenda.Equals("Aguardando Pagamento"))
            {
                if (venda.StatusVenda.Equals("Pagamento Aprovado") || venda.StatusVenda.Equals("Cancelada"))
                {
                    return true;
                }
            }
            if (vendaBanco.StatusVenda.Equals("Pagamento Aprovado"))
            {
                if (venda.StatusVenda.Equals("Enviado para Transportadora") || venda.StatusVenda.Equals("Cancelada"))
                {
                    return true;
                }
            }
            if (vendaBanco.StatusVenda.Equals("Enviado para Transportadora"))
            {
                if (venda.StatusVenda.Equals("Entregue"))
                {
                    return true;
                }
            }
            return false;
        }
    }

}
