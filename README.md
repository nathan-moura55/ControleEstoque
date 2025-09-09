### Sistema simples de controle de estoque desenvolvido em C# com interface de linha de comando (console), voltado para fins didáticos e gerenciamento básico de produtos.

## Funcionalidades
- Listar produtos cadastrados

- Entrada de estoque (adicionar unidades a um produto)

- Saída de estoque (remover unidades de um produto)

- Alerta de estoque abaixo do mínimo

## Estrutura do Projeto

bash
```
ControleEstoque/
├── Program.cs
├── models/
│   └── Produto.cs
├── services/
│   └── Estoque.cs
├── ControleEstoque.csproj
└── README.md
```

## Exemplo de uso

bash
```
===== Controle de Estoque =====
1 - Listar Produtos
2 - Entrada de Estoque
3 - Saída de Estoque
0 - Sair
Escolha:
```

## Lógica do sistema
Os produtos são armazenados em uma lista (List<Produto>) na classe Estoque.

Cada Produto possui:

- Id

- Nome

- Quantidade

- Estoque Minimo

O sistema permite adicionar ou remover quantidade de produtos.

Se a quantidade ficar abaixo do mínimo, é exibido um alerta.

##
>[!note]
> O projeto ainda está sujeito a modificações e melhorias.
