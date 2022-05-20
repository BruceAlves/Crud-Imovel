namespace CrudImovel.Data
{
    public class Imovel
    {
        public int Id { get; set; }
        public string Cep { get; set; }
        public string  Rua { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public double ValorAluguel { get; set; }
        public string TipoImovel { get; set; }
    }
}
