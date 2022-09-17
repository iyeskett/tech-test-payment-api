namespace PaymentApi.Entities
{
    public class Venda
    {
        public int Id { get; set; }
        public int IdVendedor { get; set; }
        public DateTime Data { get; set; }
        public string Itens { get; set; }
        public string StatusVenda { get; set; }
    }
}
