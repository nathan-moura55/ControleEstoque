using Estoque.Dominio.Models;
using System.Collections.Generic;

namespace Estoque.Dominio.Interfaces
{
    public interface IProdutoRepositorio
    {
        void Adicionar(Produto produto);
        void Atualizar(Produto produto);
        void Remover(int id);
        Produto? ObterPorId(int id);
        IEnumerable<Produto> ObterTodos();
    }
}