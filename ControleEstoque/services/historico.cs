using System;
using System.Collections.Generic;

public class Historico
{
    private List<string>  logs = new();
    
    public void Registrar(string mensagem)
    {
        string log= $"[{DateTime.Now}] {mensagem}";
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
}

