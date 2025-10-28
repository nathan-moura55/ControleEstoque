﻿using System;
using System.Collections.Generic;
using Estoque.Dominio.Models;
using Estoque.Serviços;

class Program
{
    static void Main(string[] args)
    {
        ControleDeEstoque estoque = new ControleDeEstoque();
        Historico historico = new Historico();

        estoque.AdicionarProduto(new Produto(1, "Caneta azul", 20, 10));
        estoque.AdicionarProduto(new Produto(2, "Lápis", 25, 12));
        estoque.AdicionarProduto(new Produto(3, "Borracha", 10, 5));

        historico.Registrar("Produtos iniciais cadastrados.");

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
            Console.Write("Escolha uma opção: ");

            if (!int.TryParse(Console.ReadLine(), out opcaoMenu))
            {
                Console.WriteLine("Entrada inválida. Pressione uma tecla para continuar...");
                Console.ReadKey();
                continue;
            }

            switch (opcaoMenu)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("===== Selecione um usuário =====\n");

                    foreach (var usuario in usuarios)
                    {
                        Console.WriteLine($"{usuario.Id} - {usuario.Nome} ({usuario.Cargo})");
                    }

                    Console.Write("\nDigite o ID do usuário: ");
                    if (!int.TryParse(Console.ReadLine(), out int idUsuario))
                    {
                        Console.WriteLine("ID inválido. Pressione uma tecla para continuar...");
                        Console.ReadKey();
                        continue;
                    }

                    Usuario usuarioSelecionado = usuarios.Find(u => u.Id == idUsuario);

                    if (usuarioSelecionado != null)
                    {
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
                            Console.WriteLine("4 - Historico de alterações");
                            Console.WriteLine("0 - Trocar usuário");
                            Console.Write("Escolha: ");

                            if (!int.TryParse(Console.ReadLine(), out opcao))
                            {
                                Console.WriteLine("Opção inválida.");
                                continue;
                            }

                            switch (opcao)
                            {
                                case 1:
                                    Console.WriteLine("\nProdutos Cadastrados:");
                                    estoque.ListarProdutos();
                                    break;

                                case 2:
                                    Console.Write("ID do Produto: ");
                                    if (!int.TryParse(Console.ReadLine(), out int idEntrada))
                                    {
                                        Console.WriteLine("ID inválido.");
                                        break;
                                    }

                                    Console.Write("Quantidade a adicionar: ");
                                    if (!int.TryParse(Console.ReadLine(), out int qtdEntrada))
                                    {
                                        Console.WriteLine("Quantidade inválida.");
                                        break;
                                    }

                                    estoque.EntradaEstoque(idEntrada, qtdEntrada);
                                    Console.WriteLine("");
                                    historico.Registrar($"Entrada de {qtdEntrada} unidades no produto ID {idEntrada}.");
                                    break;

                                case 3:
                                    Console.Write("ID do Produto: ");
                                    if (!int.TryParse(Console.ReadLine(), out int idSaida))
                                    {
                                        Console.WriteLine("ID inválido.");
                                        break;
                                    }

                                    Console.Write("Quantidade a remover: ");
                                    if (!int.TryParse(Console.ReadLine(), out int qtdSaida))
                                    {
                                        Console.WriteLine("Quantidade inválida.");
                                        break;
                                    }

                                    try
                                    {
                                        estoque.SaidaEstoque(idSaida, qtdSaida);
                                        historico.Registrar($"Saída de {qtdSaida} unidades do produto ID {idSaida}.");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"ERRO: {ex.Message}");
                                    }
                                    break;

                                case 0:
                                    Console.WriteLine("Logout realizado com sucesso.");
                                    break;
                                
                                case 4:
                                    historico.ListarHistorico();
                                    break;

                                default:
                                    Console.WriteLine("Opção inválida.");
                                    break;
                            }

                            Console.WriteLine("\nPressione qualquer tecla para continuar...");
                            Console.ReadKey();
                            Console.Clear();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Usuário não encontrado!");
                        Console.WriteLine("Pressione qualquer tecla para continuar...");
                        Console.ReadKey();
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
}