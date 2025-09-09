using ControleEstoque.models;
using ControleEstoque.services;

class Program
{
    static void Main(string[] args)
    {
        Estoque estoque = new Estoque();

        estoque.AdicionarProduto(new Produto(1, "Caneta azul", 20, 10));
        estoque.AdicionarProduto(new Produto(2, "Lápis", 25, 12));
        estoque.AdicionarProduto(new Produto(3, "Borracha", 10, 5));

        int opcao;

        do
        {
            Console.Clear();
            Console.WriteLine("===== Controle de Estoque =====");
            Console.WriteLine("1 - Listar Produtos");
            Console.WriteLine("2 - Entrada de Estoque");
            Console.WriteLine("3 - Saída de Estoque");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha: ");
            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    Console.WriteLine("\nProdutos Cadastrados: ");
                    estoque.ListarProdutos();
                    break;

                case 2:
                    Console.Write("ID do Produto: ");
                    int idEntrada = int.Parse(Console.ReadLine());
                    Console.Write("Quantidade a adicionar: ");
                    int qtdEntrada = int.Parse(Console.ReadLine());
                    estoque.EntradaEstoque(idEntrada, qtdEntrada);
                    break;

                case 3:
                    Console.Write("ID do produto: ");
                    int idSaida = int.Parse(Console.ReadLine());
                    Console.Write("Quantidade a remover: ");
                    int qtdSaida = int.Parse(Console.ReadLine());

                    try
                    {
                        estoque.SaidaEstoque(idSaida, qtdSaida);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"ERROR: {ex.Message}");
                    }
                    break;

                case 0:
                    Console.WriteLine("Encerrando...");
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();

        } while (opcao != 0);
    }
}