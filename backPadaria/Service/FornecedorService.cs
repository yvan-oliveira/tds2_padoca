using backPadaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Service;

internal class FornecedorService
{
    public static void MenuFornecedor()
    {
        bool sair = false;

        while (!sair)
        {
            Console.WriteLine("===== GESTÃO DE FORNECEDOR =====");
            Console.WriteLine("1 - Cadastrar Fornecedor");
            Console.WriteLine("2 - Listar Todos os Fornecedores");
            Console.WriteLine("3 - Buscar Fornecedor por ID");
            Console.WriteLine("4 - Atualizar Fornecedor");
            Console.WriteLine("5 - Excluir Fornecedor");
            Console.WriteLine("0 - Sair do Programa");
            Console.WriteLine("==============================\n");

            Console.Write("Digite uma opção: ");
            string opcao = Console.ReadLine() ?? "";

            Console.Clear();

            try
            {
                switch (opcao)
                {
                    case "1":
                        CadastrarFornecedor();
                        break;

                    case "2":
                        ListarFornecedores();
                        break;

                    case "3":
                        BuscarFornecedorPorId();
                        break;

                    case "4":
                        AtualizarFornecedor();
                        break;

                    case "5":
                        ExcluirFornecedor();
                        break;

                    case "0":
                        sair = true;
                        Console.WriteLine("Saindo do programa...");
                        break;

                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        VoltarParaMenu();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nOcorreu um erro: {ex.Message}");
                VoltarParaMenu();
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

    private static Fornecedor? BuscarPorIdValidado()
    {
        int id = LerInteiro("Digite o ID do Fornecedor: ", 1);
        Fornecedor fornecedor = Fornecedor.ListarPorId(id);

        if (fornecedor == null)
            Console.WriteLine($"\nFornecedor com ID {id} não encontrado.");

        return fornecedor;
    }

    private static void ExibirFornecedor(Fornecedor fornecedor)
    {
        Console.WriteLine($"\nID: {fornecedor.Id}");
        Console.WriteLine($"Nome: {fornecedor.Nome}");
        Console.WriteLine($"CNPJ: {fornecedor.CNPJ}");
        Console.WriteLine($"Telefone: {fornecedor.Telefone}");
        Console.WriteLine("-----------------------------");
    }

    // ===== CADASTRAR =====

    private static void CadastrarFornecedor()
    {
        Console.WriteLine("-=-=-=-=-=-=- CADASTRAR FORNECEDOR -=-=-=-=-=-=-\n");

        string nome = LerTexto("Digite o nome do Fornecedor: ");
        string CNPJ = LerTexto("Digite o CNPJ do Fornecedor: ");
        string telefone = LerTexto("Digite o telefone do Fornecedor: ");

        Fornecedor novoFornecedor = new Fornecedor(
            nome: nome,
            cnpj: CNPJ,
            telefone: telefone
        );

        int resultado = Fornecedor.Inserir(novoFornecedor);

        if (resultado > 0)
            Console.WriteLine("\nFornecedor cadastrado com sucesso!");
        else
            Console.WriteLine("\nNão foi possível cadastrar o Fornecedor.");

        VoltarParaMenu();
    }

    // ===== LISTAR =====

    private static void ListarFornecedores()
    {
        Console.WriteLine("-=-=-=-=-=-=- LISTA DE FornecedorS -=-=-=-=-=-=-\n");

        List<Fornecedor> lista = Fornecedor.ListarTodos();

        if (lista.Count == 0)
        {
            Console.WriteLine("Nenhum Funcionaário cadastrado no sistema.");
        }
        else
        {
            foreach (Fornecedor Fornecedor in lista)
            {
                ExibirFornecedor(Fornecedor);
                Console.WriteLine();
            }
        }

        VoltarParaMenu();
    }

    // ===== BUSCAR POR ID =====

    private static void BuscarFornecedorPorId()
    {
        Console.WriteLine("-=-=-=-=-=-=- BUSCAR Fornecedor POR ID -=-=-=-=-=-=-\n");

        Fornecedor? Fornecedor = BuscarPorIdValidado();

        if (Fornecedor != null)
            ExibirFornecedor(Fornecedor);

        VoltarParaMenu();
    }

    // ===== ATUALIZAR =====

    private static void AtualizarFornecedor()
    {
        Console.WriteLine("-=-=-=-=-=-=- ATUALIZAR Fornecedor -=-=-=-=-=-=-\n");

        Fornecedor? Fornecedor = BuscarPorIdValidado();

        if (Fornecedor == null)
        {
            VoltarParaMenu();
            return;
        }

        bool finalizar = false;

        while (!finalizar)
        {
            Console.Clear();

            Console.WriteLine("-=-=-=-=-=-=- ATUALIZAR FORNECEDOR -=-=-=-=-=-=-");
            Console.WriteLine("\nEstado atual:");
            ExibirFornecedor(Fornecedor);

            Console.WriteLine("\n1 - Alterar o nome");
            Console.WriteLine("2 - Alterar o telefone");
            Console.WriteLine("3 - Salvar alterações");
            Console.WriteLine("0 - Cancelar alterações\n");

            Console.Write("Digite uma opção: ");
            string opcao = Console.ReadLine() ?? "";

            switch (opcao)
            {
                case "1":
                    Fornecedor.Nome = LerTexto("Digite o novo nome: ");
                    break;

                case "2":
                    Fornecedor.Telefone = LerTexto("Digite o novo telefone: ");
                    break;

                case "3":
                    int resultado = Fornecedor.AtualizarFornecedor(Fornecedor);

                    if (resultado > 0)
                        Console.WriteLine("\nFornecedor atualizado com sucesso!");
                    else
                        Console.WriteLine("\nNão foi possível atualizar o Fornecedor.");

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

    private static void ExcluirFornecedor()
    {
        Console.WriteLine("-=-=-=-=-=-=- EXCLUIR Fornecedor -=-=-=-=-=-=-\n");

        Fornecedor? Fornecedor = BuscarPorIdValidado();

        if (Fornecedor == null)
        {
            VoltarParaMenu();
            return;
        }

        ExibirFornecedor(Fornecedor);

        Console.Write("\nDeseja realmente excluir este Fornecedor? (S/N): ");
        string confirmacao = (Console.ReadLine() ?? "").Trim();

        if (confirmacao.Equals("S", StringComparison.OrdinalIgnoreCase))
        {
            int resultado = Fornecedor.Deletar(Fornecedor);

            if (resultado > 0)
                Console.WriteLine("\nFornecedor excluído com sucesso!");
            else
                Console.WriteLine("\nNão foi possível excluir o Fornecedor.");
        }
        else
        {
            Console.WriteLine("\nExclusão cancelada.");
        }

        VoltarParaMenu();
    }
}
