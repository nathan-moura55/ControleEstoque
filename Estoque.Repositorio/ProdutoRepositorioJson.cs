using Estoque.Dominio.Interfaces;
using Estoque.Dominio.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Estoque.Repositorio
{
    public class ProdutoRepositorioJson : IProdutoRepositorio
    {
        private readonly string arquivo = "C:/Users/hp/Documents/dev/controleEstoque/Estoque.Repositorio/data/produto.json";
        private List<Produto> produtos = new List<Produto>();

        public ProdutoRepositorioJson()
        {
            if (File.Exists(arquivo))
            {
                var json = File.ReadAllText(arquivo);
                produtos = JsonSerializer.Deserialize<List<Produto>>(json) ?? new List<Produto>();
            }
        }

        private void Salvar()
        {
            var json = JsonSerializer.Serialize(produtos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(arquivo, json);
        }

        public void Adicionar(Produto produto)
        {
            var existente = produtos.FirstOrDefault(p => p.Id == produto.Id && p.Nome == produto.Nome);

            if (existente != null)
            {
                existente.AdicionarEstoque(produto.Quantidade);
            }
            else
            {
                produtos.Add(produto);
            }

            Salvar();
        }

        public void Atualizar(Produto produto)
        {
            var index = produtos.FindIndex(p => p.Id == produto.Id);
            if (index >= 0)
            {
                produtos[index] = produto;
                Salvar();
            }
        }

        public void Remover(int id)
        {
            produtos.RemoveAll(p => p.Id == id);
            Salvar();
        }

        public Produto? ObterPorId(int id)
        {
            return produtos.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Produto> ObterTodos()
        {
            return produtos;
        }
    }
}