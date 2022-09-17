using PaymentApi.Entities;

namespace PaymentApi
{
    public class PaymentDto
    {
        public int IdVenda { get; set; }
        public DateTime Data { get; set; }
        public string Itens { get; set; }
        public string StatusVenda { get; set; }

        public int IdVendedor { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public void AdicionaVenda(Venda venda)
        {
            IdVenda = venda.Id;
            Data = venda.Data;
            Itens = venda.Itens;
            StatusVenda = venda.StatusVenda;

        }

        public void AdicionaVendedor(Vendedor vendedor)
        {
            IdVendedor = vendedor.Id;
            Cpf = vendedor.Cpf;
            Nome = vendedor.Nome;
            Email = vendedor.Email;
            Telefone = vendedor.Telefone;

        }
    }
}
