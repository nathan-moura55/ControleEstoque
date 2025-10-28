using System;
using System.Collections.Generic;
using System.Linq;
using Estoque.Dominio.Models;


namespace Estoque.Serviços
{
    public class ControleDeEstoque
    {
        private List<Produto> produtos = new List<Produto>();

        public void AdicionarProduto(Produto produto)
        {
            produtos.Add(produto);
        }

        public Produto BuscarProduto(int id)
        {
            return produtos.FirstOrDefault(p => p.Id == id);
        }

        public void EntradaEstoque(int id, int quantidade)
        {
            var produto = BuscarProduto(id);
            if (produto != null)
            {
                produto.AdicionarEstoque(quantidade);
            }
            else
            {
                Console.WriteLine("Produto não encontrado.");
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
            }
            else
            {
                Console.WriteLine("Produto não encontrado.");
            }
        }

        public void ListarProdutos()
        {
            foreach (var produto in produtos)
            {
                Console.WriteLine(produto);
            }
        }
    }
}