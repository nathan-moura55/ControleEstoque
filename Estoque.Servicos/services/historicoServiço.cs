using System;
using System.Collections.Generic;

public class Historico
{
    private List<string> logs = new();

    public void Registrar(string mensagem)
    {
        string log = $"[{DateTime.Now}] {mensagem}";
        logs.Add(log);
        Console.WriteLine(log);
    }

    public void ListarHistorico()
    {
        Console.WriteLine("\n===== Histórico de alteração! =====");
        foreach (var log in logs)
        {
            Console.WriteLine(log);
        }
        Console.WriteLine("\n====================================");
    }

    public void SalvarEmArquivo(string usuarioNome)
    {
        try
        {
            string pasta = @"C:\Users\hp\Documents\dev\controleEstoque\Estoque.Repositorio\data\history";

            if (!Directory.Exists(pasta))
            {
                Directory.CreateDirectory(pasta);
            }

            string caminhoArquivo = Path.Combine(pasta, $"historico_{usuarioNome}_{DateTime.Now:yyyyMMdd_HHmmss}.txt");

            File.WriteAllLines(caminhoArquivo, logs);

            Console.WriteLine($"Histórico salvo em: {caminhoArquivo}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar histórico: {ex.Message}");
        }
        finally
        {
            logs.Clear();
        }
    }
}



