using backPadaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
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
                            BuscarProdutoPorId(); break;
                        case "4":
                            AtualizarProduto(); break;
                        case "5":
                            ExcluirProduto(); break;
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

        private static Produto? BuscarPorIdValidado()
        {
            int id = LerInteiro("Digite o ID do cliente: ", 1);
            Produto produto = Produto.ListarPorId(id);

            if (produto == null)
                Console.WriteLine($"\nProduto com ID {id} não encontrado.");

            return produto;
        }

        private static void ExibirProduto(Produto produto)
        {
            Console.WriteLine($"\nID: {produto.Id}");
            Console.WriteLine($"Nome: {produto.Nome}");
            Console.WriteLine($"Preço: {produto.Preco}");
            Console.WriteLine($"Categoria: {produto.Categoria}");
            Console.WriteLine($"Fabricado: {produto.Fabricado}");
            Console.WriteLine($"Marca id: {produto.MarcaId}");
            Console.WriteLine("-----------------------------");
        }

        // ===== CADASTRAR =====

        private static void CadastrarMarca()
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

        // ===== LISTAR =====

        private static void ListarProdutos()
        {
            Console.WriteLine("-=-=-=-=-=-=- LISTA DE PRODUTOS -=-=-=-=-=-=-\n");

            List<Produto> lista = Produto.ListarTodos();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhum produto cadastrado no sistema.");
            }
            else
            {
                foreach (Produto produto in lista)
                {
                    ExibirProduto(produto);
                    Console.WriteLine();
                }
            }

            VoltarParaMenu();
        }

        // ===== BUSCAR POR ID =====

        private static void BuscarProdutoPorId()
        {
            Console.WriteLine("-=-=-=-=-=-=- BUSCAR PRODUTO POR ID -=-=-=-=-=-=-\n");

            Produto? produto = BuscarPorIdValidado();

            if (produto != null)
                ExibirProduto(produto);

            VoltarParaMenu();
        }

        // ===== ATUALIZAR =====

        private static void AtualizarProduto()
        {
            Console.WriteLine("-=-=-=-=-=-=- ATUALIZAR PRODUTO -=-=-=-=-=-=-\n");

            Produto? produto = BuscarPorIdValidado();

            if (produto == null)
            {
                VoltarParaMenu();
                return;
            }

            bool finalizar = false;

            while (!finalizar)
            {
                Console.Clear();

                Console.WriteLine("-=-=-=-=-=-=- ATUALIZAR PRODUTO -=-=-=-=-=-=-");
                Console.WriteLine("\nEstado atual:");
                ExibirProduto(produto);

                Console.WriteLine("\n1 - Nome do Produto");
                Console.WriteLine("2 - Preço do Produto");
                Console.WriteLine("3 - Categoria do Produto");
                Console.WriteLine("4 - Status do Produto Fabricado");
                Console.WriteLine("5 - Marca do Produto");
                Console.WriteLine("0 - Cancelar");
                Console.Write("Escolha uma opção: ");


                Console.Write("Digite uma opção: ");
                string opcao = Console.ReadLine() ?? "";

                switch (opcao)
                {
                    case "1":
                        produto.Nome = LerTexto("Digite o novo nome: ");
                        break;

                    case "2":
                        Console.WriteLine("Digite o novo preço: ");
                        produto.Preco = decimal.Parse(Console.ReadLine()!);
                        break;

                    case "3":
                        produto.Categoria = LerTexto("Digite a nova categoria: ");
                        break;

                    case "4":
                        produto.Categoria = LerTexto("Digite a nova categoria: ");
                        produto.Fabricado = bool.Parse(Console.ReadLine()!);

                        break;

                    case "5":
                        int resultado = Produto.AtualizarProduto(produto);

                        if (resultado > 0)
                            Console.WriteLine("\nProduto atualizado com sucesso!");
                        else
                            Console.WriteLine("\nNão foi possível atualizar o produto.");

                        finalizar = true;
                        break;

                    case "0":
                        Console.WriteLine("\nAlterações canceladas.");
                        finalizar = true;
                        break;

                    default:
                        Console.WriteLine("\nOpção inválida.");
                        Thread.Sleep(1500);
                        break;
                }
            }

            VoltarParaMenu();
        }

        // ===== EXCLUIR =====

        private static void ExcluirProduto()
        {
            Console.WriteLine("-=-=-=-=-=-=- EXCLUIR PRODUTO -=-=-=-=-=-=-\n");

            Produto? produto = BuscarPorIdValidado();

            if (produto == null)
            {
                VoltarParaMenu();
                return;
            }

            ExibirProduto(produto);

            Console.Write("\nDeseja realmente excluir este produto? (S/N): ");
            string confirmacao = (Console.ReadLine() ?? "").Trim();

            if (confirmacao.Equals("S", StringComparison.OrdinalIgnoreCase))
            {
                int resultado = Produto.Deletar(produto);

                if (resultado > 0)
                    Console.WriteLine("\nproduto excluído com sucesso!");
                else
                    Console.WriteLine("\nNão foi possível excluir o produto.");
            }
            else
            {
                Console.WriteLine("\nExclusão cancelada.");
            }

            VoltarParaMenu();
        }
    }
}
