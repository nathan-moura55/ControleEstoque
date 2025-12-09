using Estoque.Dominio.Models;
using Estoque.Dominio.Interfaces;

namespace Estoque.Servicos
{
    public class ControleDeEstoque
    {
        private readonly IProdutoRepositorio _repositorio;

        public ControleDeEstoque(IProdutoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public void AdicionarProduto(Produto produto)
        {
            var existente = _repositorio.ObterTodos()
                                        .FirstOrDefault(p => p.Id == produto.Id || p.Nome.Equals(produto.Nome, StringComparison.OrdinalIgnoreCase));

            if (existente != null)
                throw new Exception("Produto com mesmo ID ou nome já existe.");

            _repositorio.Adicionar(produto);
        }

        public Produto BuscarProduto(int id)
        {
            return _repositorio.ObterPorId(id)!;
        }

        public void EntradaEstoque(int id, int quantidade)
        {
            var produto = BuscarProduto(id);
            if (produto != null)
            {
                produto.AdicionarEstoque(quantidade);
                _repositorio.Atualizar(produto);
            }
        }

        public void SaidaEstoque(int id, int quantidade)
        {
            var produto = BuscarProduto(id);
            if (produto != null)
            {
                produto.RemoverEstoque(quantidade);
                if (produto.AbaixoDoMinimo())
                {
                    Console.WriteLine($"[ALERTA] Produto '{produto.Nome}' está abaixo do estoque mínimo.");
                }
                _repositorio.Atualizar(produto);
            }
        }

        public void RemoverProduto(int id)
        {
            var produto = _repositorio.ObterPorId(id);

            if (produto == null)
                throw new Exception("Produto não encontrado.");

            if (produto.Quantidade > 0)
                throw new Exception("Não é possível excluir um produto com estoque.");

            _repositorio.Remover(id);
        }

        public void ListarProdutos()
        {
            foreach (var produto in _repositorio.ObterTodos())
            {
                Console.WriteLine(produto);
            }
        }
    }
}