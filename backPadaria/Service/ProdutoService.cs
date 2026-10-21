using backPadaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Service
{
    internal class ProdutoService
    {
        public static void MenuProduto()
        {
            bool sair = false;
            while (!sair)
            {
                Console.Clear();
                Console.WriteLine("===== GESTÃO DE PRODUTOS ===== ");
                Console.WriteLine("1 - Cadastrar Produto");
                Console.WriteLine("2 - Listar Todos os Produtos");
                Console.WriteLine("3 - Buscar Produto por ID");
                Console.WriteLine("4 - Atualizar Produto");
                Console.WriteLine("5 - Excluir Produto");
                Console.WriteLine("0 - Sair do Programa");

                Console.WriteLine(" =============================");
                Console.Write("Digite uma opção: ");

                string opcao = Console.ReadLine();
                Console.Clear();

                try
                {
                    switch (opcao)
                    {
                        case "1":
                            CadastrarProduto(); break;
                        case "2":
                            ListarProdutos(); break;
                        case "3":
                            ListarProdutoPorId(); break;
                        case "4":
                            AtualizarProduto(); break;
                        case "5":
                            DeletarProduto(); break;
                        case "0":
                            sair = true;
                            Console.WriteLine("Encerrando o programa..."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        // ===== MÉTODOS AUXILIARES =====

        private static void VoltarParaMenu()
        {
            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
            Console.Clear();
        }

        private static string LerTexto(string mensagem)
        {
            while (true)
            {
                Console.Write(mensagem);
                string texto = (Console.ReadLine() ?? "").Trim();

                if (!string.IsNullOrWhiteSpace(texto))
                    return texto;

                Console.WriteLine("O campo não pode ficar vazio.\n");
            }
        }

        private static int LerInteiro(string mensagem, int minimo = 0)
        {
            while (true)
            {
                Console.Write(mensagem);
                string entrada = Console.ReadLine() ?? "";

                if (int.TryParse(entrada, out int valor) && valor >= minimo)
                    return valor;

                Console.WriteLine(
                    $"Digite um número inteiro válido, maior ou igual a {minimo}.\n"
                );
            }
        }

        private static Cliente? BuscarPorIdValidado()
        {
            int id = LerInteiro("Digite o ID do cliente: ", 1);
            Cliente cliente = Cliente.ListarPorId(id);

            if (cliente == null)
                Console.WriteLine($"\nCliente com ID {id} não encontrado.");

            return cliente;
        }

        private static void ExibirCliente(Cliente cliente)
        {
            Console.WriteLine($"\nID: {cliente.Id}");
            Console.WriteLine($"Nome: {cliente.Nome}");
            Console.WriteLine($"CPF: {cliente.Cpf}");
            Console.WriteLine($"Telefone: {cliente.Telefone}");
            Console.WriteLine($"Endereço: {cliente.Endereco}");
            Console.WriteLine($"Pontos: {cliente.Pontos}");
            Console.WriteLine("-----------------------------");
        }

        private static void CadastrarProduto()
        {
            Console.WriteLine("-=-=-=-=-=-=- CADASTRAR PRODUTO -=-=-=-=-=-=-\n");

            string nomeProduto = LerTexto("Digite o nome do Produto: ");

            Console.WriteLine("Digite o preco do Produto: ");
            decimal precoProduto = decimal.Parse(Console.ReadLine()!);

            string categoriaProduto = LerTexto("Digite a categoria do Produto: ");

            Console.Write("Digite se o Produto foi Fabricado (true/false): ");
            bool fabricadoProduto = bool.Parse(Console.ReadLine()!);

            int idMarca = LerInteiro("Digite o ID da Marca do Produto: ", 1);  

            Produto novoProduto = new Produto(nome: nomeProduto, preco: precoProduto, categoria: categoriaProduto, fabricado: fabricadoProduto, marcaId: idMarca
            );

            int resultado = Produto.Inserir(novoProduto);

            if (resultado > 0)
                Console.WriteLine("\nProduto cadastrado com sucesso!");
            else
                Console.WriteLine("\nNão foi possível cadastrar o Produto.");

            VoltarParaMenu();
        }
    }
}
