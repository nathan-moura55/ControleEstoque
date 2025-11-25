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

        public void ListarProdutos()
        {
            foreach (var produto in _repositorio.ObterTodos())
            {
                Console.WriteLine(produto);
            }
        }
    }
}