namespace Estoque.Dominio.Models
{
    public class Produto
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public int Quantidade { get; private set; }
        public int EstoqueMinimo { get; private set; }

        public Produto(int id, string nome, int quantidade, int estoqueMinimo)
        {
            Id = id;
            Nome = nome;
            Quantidade = quantidade;
            EstoqueMinimo = estoqueMinimo;
        }

        public void AdicionarEstoque(int qtd) => Quantidade += qtd;

        public void RemoverEstoque(int qtd)
        {
            if (qtd > Quantidade)
                throw new InvalidOperationException("Estoque insuficiente.");
            Quantidade -= qtd;
        }

        public void AtualizarNome(string novoNome) => Nome = novoNome;
        public void AtualizarEstoqueMinimo(int novoMinimo) => EstoqueMinimo = novoMinimo;

        public bool AbaixoDoMinimo() => Quantidade < EstoqueMinimo;

        public override string ToString() =>
            $"ID: {Id} | NOME: {Nome} | QUANTIDADE: {Quantidade} | MÍNIMO: {EstoqueMinimo}";
    }
}