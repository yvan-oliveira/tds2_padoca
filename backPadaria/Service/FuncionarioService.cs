using backPadaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Service;

internal class FuncionarioService
{
    public static void MenuFuncionario()
    {
        bool sair = false;

        while (!sair)
        {
            Console.WriteLine("===== GESTÃO DE FUNCIONÁRIOS =====");
            Console.WriteLine("1 - Cadastrar Funcionários");
            Console.WriteLine("2 - Listar Todos os Funcionários");
            Console.WriteLine("3 - Buscar Funcionário por ID");
            Console.WriteLine("4 - Atualizar Funcionário");
            Console.WriteLine("5 - Excluir Funcionário");
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
                        CadastrarFuncionario();
                        break;

                    case "2":
                        ListarFuncionarios();
                        break;

                    case "3":
                        BuscarFuncionarioPorId();
                        break;

                    case "4":
                        AtualizarFuncionario();
                        break;

                    case "5":
                        ExcluirFuncionario();
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

    private static Funcionario? BuscarPorIdValidado()
    {
        int id = LerInteiro("Digite o ID do Funcionário: ", 1);
        Funcionario funcionario = Funcionario.ListarPorId(id);

        if (funcionario == null)
            Console.WriteLine($"\nFuncionário com ID {id} não encontrado.");

        return funcionario;
    }

    private static void ExibirFuncionario(Funcionario funcionario)
    {
        Console.WriteLine($"\nID: {funcionario.Id}");
        Console.WriteLine($"Nome: {funcionario.Nome}");
        Console.WriteLine($"CPF: {funcionario.Cpf}");
        Console.WriteLine($"Cargo: {funcionario.Cargo}");
        Console.WriteLine("-----------------------------");
    }

    // ===== CADASTRAR =====

    private static void CadastrarFuncionario()
    {
        Console.WriteLine("-=-=-=-=-=-=- CADASTRAR FUNCIONARIO -=-=-=-=-=-=-\n");

        string nome = LerTexto("Digite o nome do Funcionário: ");
        string cpf = LerTexto("Digite o CPF do Funcionário: ");
        string cargo = LerTexto("Digite o Cargo do Funcionário: ");

        Funcionario novoFuncionario = new Funcionario(
            nome: nome,
            cpf: cpf,
            cargo: cargo
        );

        int resultado = Funcionario.Inserir(novoFuncionario);

        if (resultado > 0)
            Console.WriteLine("\nFuncionário cadastrado com sucesso!");
        else
            Console.WriteLine("\nNão foi possível cadastrar o Funcionário.");

        VoltarParaMenu();
    }

    // ===== LISTAR =====

    private static void ListarFuncionarios()
    {
        Console.WriteLine("-=-=-=-=-=-=- LISTA DE FUNCIONÁRIOS -=-=-=-=-=-=-\n");

        List<Funcionario> lista = Funcionario.ListarTodos();

        if (lista.Count == 0)
        {
            Console.WriteLine("Nenhum Funcionaário cadastrado no sistema.");
        }
        else
        {
            foreach (Funcionario funcionario in lista)
            {
                ExibirFuncionario(funcionario);
                Console.WriteLine();
            }
        }

        VoltarParaMenu();
    }

    // ===== BUSCAR POR ID =====

    private static void BuscarFuncionarioPorId()
    {
        Console.WriteLine("-=-=-=-=-=-=- BUSCAR FUNCIONÁRIO POR ID -=-=-=-=-=-=-\n");

        Funcionario? funcionario = BuscarPorIdValidado();

        if (funcionario != null)
            ExibirFuncionario(funcionario);

        VoltarParaMenu();
    }

    // ===== ATUALIZAR =====

    private static void AtualizarFuncionario()
    {
        Console.WriteLine("-=-=-=-=-=-=- ATUALIZAR FUNCIONÁRIO -=-=-=-=-=-=-\n");

        Funcionario? funcionario = BuscarPorIdValidado();

        if (funcionario == null)
        {
            VoltarParaMenu();
            return;
        }

        bool finalizar = false;

        while (!finalizar)
        {
            Console.Clear();

            Console.WriteLine("-=-=-=-=-=-=- ATUALIZAR FUNCIONÁRIO -=-=-=-=-=-=-");
            Console.WriteLine("\nEstado atual:");
            ExibirFuncionario(funcionario);

            Console.WriteLine("\n1 - Alterar o nome");
            Console.WriteLine("2 - Alterar o cargo");
            Console.WriteLine("3 - Salvar alterações");
            Console.WriteLine("0 - Cancelar alterações\n");

            Console.Write("Digite uma opção: ");
            string opcao = Console.ReadLine() ?? "";

            switch (opcao)
            {
                case "1":
                    funcionario.Nome = LerTexto("Digite o novo nome: ");
                    break;

                case "2":
                    funcionario.Cargo = LerTexto("Digite o novo cargo: ");
                    break;

                case "3":
                    int resultado = Funcionario.AtualizarFuncionario(funcionario);

                    if (resultado > 0)
                        Console.WriteLine("\nFuncionário atualizado com sucesso!");
                    else
                        Console.WriteLine("\nNão foi possível atualizar o Funcionário.");

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
    
    private static void ExcluirFuncionario()
    {
        Console.WriteLine("-=-=-=-=-=-=- EXCLUIR FUNCIONARIO -=-=-=-=-=-=-\n");

        Funcionario? funcionario = BuscarPorIdValidado();

        if (funcionario == null)
        {
            VoltarParaMenu();
            return;
        }

        ExibirFuncionario(funcionario);

        Console.Write("\nDeseja realmente excluir este funcionario? (S/N): ");
        string confirmacao = (Console.ReadLine() ?? "").Trim();

        if (confirmacao.Equals("S", StringComparison.OrdinalIgnoreCase))
        {
            int resultado = Funcionario.Deletar(funcionario);

            if (resultado > 0)
                Console.WriteLine("\nFuncionário excluído com sucesso!");
            else
                Console.WriteLine("\nNão foi possível excluir o funcionário.");
        }
        else
        {
            Console.WriteLine("\nExclusão cancelada.");
        }

        VoltarParaMenu();
    }

    
}
