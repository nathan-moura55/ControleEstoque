using Estoque.Dominio.Models;
using Estoque.Repositorio;
using Estoque.Servicos;

class Program
{
    static void Main(string[] args)
    {
        var repositorio = new ProdutoRepositorioJson();
        var estoque = new ControleDeEstoque(repositorio);
        var historico = new Historico();

        var produtosExistentes = repositorio.ObterTodos().ToList();
        if (!produtosExistentes.Any())
        {
            estoque.AdicionarProduto(new Produto(1, "Caneta azul", 20, 10));
            estoque.AdicionarProduto(new Produto(2, "Lápis", 25, 12));
            estoque.AdicionarProduto(new Produto(3, "Borracha", 10, 5));
            historico.Registrar("Produtos iniciais cadastrados.");
        }
        else
        {
            historico.Registrar("Produtos carregados do repositório.");
        }

        List<Usuario> usuarios = new List<Usuario>
        {
            new Usuario(1, "Pedro", "Gerente"),
            new Usuario(2, "João", "Ajudante de Estoque")
        };

        int opcaoMenu = -1;

        while (opcaoMenu != 2)
        {
            Console.Clear();
            Console.WriteLine("===== Sistema de Controle de Estoque =====");
            Console.WriteLine("1 - Entrar");
            Console.WriteLine("2 - Sair");

            opcaoMenu = LerOpcao("Escolha uma opção: ");

            switch (opcaoMenu)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("===== Selecione um usuário =====\n");

                    foreach (var usuario in usuarios)
                        Console.WriteLine($"{usuario.Id} - {usuario.Nome} ({usuario.Cargo})");

                    int idUsuario = LerOpcao("\nDigite o ID do usuário: ");
                    Usuario usuarioSelecionado = usuarios.Find(u => u.Id == idUsuario);

                    if (usuarioSelecionado == null)
                    {
                        Console.WriteLine("Usuário não encontrado!");
                        Console.WriteLine("Pressione qualquer tecla para continuar...");
                        Console.ReadKey();
                        continue;
                    }

                    Console.Clear();
                    Console.WriteLine($"Bem-vindo, {usuarioSelecionado.Nome} ({usuarioSelecionado.Cargo})\n");

                    int opcao = -1;
                    while (opcao != 0)
                    {
                        Console.WriteLine($"Usuário: {usuarioSelecionado.Nome} | Cargo: {usuarioSelecionado.Cargo}");
                        Console.WriteLine("===== Controle de Estoque =====");
                        Console.WriteLine("1 - Listar Produtos");
                        Console.WriteLine("2 - Entrada de Estoque");
                        Console.WriteLine("3 - Saída de Estoque");
                        Console.WriteLine("5 - Gerenciar produto");
                        Console.WriteLine("6 - Histórico de alterações");
                        Console.WriteLine("0 - Trocar usuário");

                        opcao = LerOpcao("Escolha: ");

                        switch (opcao)
                        {
                            case 1:
                                Console.WriteLine("\nProdutos Cadastrados:");
                                estoque.ListarProdutos();
                                break;

                            case 2:
                                int idEntrada = LerOpcao("ID do Produto: ");
                                int qtdEntrada = LerOpcao("Quantidade a adicionar: ");
                                estoque.EntradaEstoque(idEntrada, qtdEntrada);
                                historico.Registrar($"Entrada de {qtdEntrada} unidades no produto ID {idEntrada}.");
                                Console.WriteLine("Entrada registrada com sucesso!");
                                break;

                            case 3:
                                int idSaida = LerOpcao("ID do Produto: ");
                                int qtdSaida = LerOpcao("Quantidade a remover: ");

                                try
                                {
                                    estoque.SaidaEstoque(idSaida, qtdSaida);
                                    historico.Registrar($"Saída de {qtdSaida} unidades do produto ID {idSaida}.");
                                    Console.WriteLine("Saída registrada com sucesso!");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"ERRO: {ex.Message}");
                                }
                                break;

                            case 5:
                                GerenciarProdutos(estoque, historico);
                                break;

                            case 6:
                                historico.ListarHistorico();
                                break;

                            case 0:
                                historico.SalvarEmArquivo(usuarioSelecionado.Nome);
                                Console.WriteLine("Logout realizado com sucesso.");
                                break;

                            default:
                                Console.WriteLine("Opção inválida!");
                                break;
                        }

                        Console.WriteLine("\nPressione qualquer tecla para continuar...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    break;

                case 2:
                    Console.WriteLine("Encerrando sistema...");
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static int LerOpcao(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Digite algo!");
                continue;
            }

            if (!int.TryParse(input, out int opcao))
            {
                Console.WriteLine("Opção inválida, digite um número.");
                continue;
            }

            return opcao;
        }
    }
    static void GerenciarProdutos(ControleDeEstoque estoque, Historico historico)
    {
        int opcaoGerenciar = -1;
        while (opcaoGerenciar != 0)
        {
            Console.Clear();
            Console.WriteLine("===== Gerenciar Produtos =====");
            Console.WriteLine("1 - Cadastrar Produto");
            Console.WriteLine("2 - Remover Produto");
            Console.WriteLine("0 - Voltar");

            opcaoGerenciar = LerOpcao("Escolha: ");

            switch (opcaoGerenciar)
            {
                case 1:
                    int id = LerOpcao("ID do Produto: ");
                    Console.Write("Nome do Produto: ");
                    string? nome = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(nome))
                    {
                        Console.WriteLine("Nome inválido!");
                        Console.ReadKey();
                        break;
                    }
                    int qtd = LerOpcao("Quantidade: ");
                    int min = LerOpcao("Estoque mínimo: ");

                    try
                    {
                        estoque.AdicionarProduto(new Produto(id, nome, qtd, min));
                        historico.Registrar($"Produto cadastrado: {nome} (ID {id})");
                        Console.WriteLine("Produto cadastrado com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro: {ex.Message}");
                    }
                    Console.ReadKey();
                    break;

                case 2:
                    Console.WriteLine("[AVISO] Para remover os produtos, o estoque deve ser igual a zero.");
                    int idRemover = LerOpcao("ID do Produto a remover: ");
                    try
                    {
                        estoque.RemoverProduto(idRemover);
                        historico.Registrar($"Produto removido: ID {idRemover}");
                        Console.WriteLine("Produto removido com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro: {ex.Message}");
                    }
                    Console.ReadKey();
                    break;

                case 0:
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }
        }
    }
}