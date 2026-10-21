
using backPadaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace backPadaria.Services;

internal class ClienteService
{
    public static void MenuCliente()
    {
        bool sair = false;

        while (!sair)
        {
            Console.WriteLine("===== GESTÃO DE CLIENTES =====");
            Console.WriteLine("1 - Cadastrar Clientes");
            Console.WriteLine("2 - Listar Todos os Clientes");
            Console.WriteLine("3 - Buscar Cliente por ID");
            Console.WriteLine("4 - Atualizar Cliente");
            Console.WriteLine("5 - Excluir Cliente");
            Console.WriteLine("6 - Visualizar Pontuação");
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
                        CadastrarCliente();
                        break;

                    case "2":
                        ListarClientes();
                        break;

                    case "3":
                        BuscarClientePorId();
                        break;

                    case "4":
                        AtualizarCliente();
                        break;

                    case "5":
                        ExcluirCliente();
                        break;

                    case "6":
                        VisualizarPontuacao();
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

    // ===== CADASTRAR =====

    private static void CadastrarCliente()
    {
        Console.WriteLine("-=-=-=-=-=-=- CADASTRAR CLIENTE -=-=-=-=-=-=-\n");

        string nome = LerTexto("Digite o nome do cliente: ");
        string endereco = LerTexto("Digite o endereço do cliente: ");
        string cpf = LerTexto("Digite o CPF do cliente: ");
        string telefone = LerTexto("Digite o telefone do cliente: ");
        int pontos = LerInteiro("Digite a quantidade de pontos: ");

        Cliente novoCliente = new Cliente(
            nome: nome,
            cpf: cpf,
            telefone: telefone,
            endereco: endereco,
            pontos: pontos
        );

        int resultado = Cliente.Inserir(novoCliente);

        if (resultado > 0)
            Console.WriteLine("\nCliente cadastrado com sucesso!");
        else
            Console.WriteLine("\nNão foi possível cadastrar o cliente.");

        VoltarParaMenu();
    }

    // ===== LISTAR =====

    private static void ListarClientes()
    {
        Console.WriteLine("-=-=-=-=-=-=- LISTA DE CLIENTES -=-=-=-=-=-=-\n");

        List<Cliente> lista = Cliente.ListarTodos();

        if (lista.Count == 0)
        {
            Console.WriteLine("Nenhum cliente cadastrado no sistema.");
        }
        else
        {
            foreach (Cliente cliente in lista)
            {
                ExibirCliente(cliente);
                Console.WriteLine();
            }
        }

        VoltarParaMenu();
    }

    // ===== BUSCAR POR ID =====

    private static void BuscarClientePorId()
    {
        Console.WriteLine("-=-=-=-=-=-=- BUSCAR CLIENTE POR ID -=-=-=-=-=-=-\n");

        Cliente? cliente = BuscarPorIdValidado();

        if (cliente != null)
            ExibirCliente(cliente);

        VoltarParaMenu();
    }

    // ===== ATUALIZAR =====

    private static void AtualizarCliente()
    {
        Console.WriteLine("-=-=-=-=-=-=- ATUALIZAR CLIENTE -=-=-=-=-=-=-\n");

        Cliente? cliente = BuscarPorIdValidado();

        if (cliente == null)
        {
            VoltarParaMenu();
            return;
        }

        bool finalizar = false;

        while (!finalizar)
        {
            Console.Clear();

            Console.WriteLine("-=-=-=-=-=-=- ATUALIZAR CLIENTE -=-=-=-=-=-=-");
            Console.WriteLine("\nEstado atual:");
            ExibirCliente(cliente);

            Console.WriteLine("\n1 - Alterar o nome");
            Console.WriteLine("2 - Alterar o endereço");
            Console.WriteLine("3 - Alterar o telefone");
            Console.WriteLine("4 - Alterar a quantidade de pontos");
            Console.WriteLine("5 - Salvar alterações");
            Console.WriteLine("0 - Cancelar alterações\n");

            Console.Write("Digite uma opção: ");
            string opcao = Console.ReadLine() ?? "";

            switch (opcao)
            {
                case "1":
                    cliente.Nome = LerTexto("Digite o novo nome: ");
                    break;

                case "2":
                    cliente.Endereco = LerTexto("Digite o novo endereço: ");
                    break;

                case "3":
                    cliente.Telefone = LerTexto("Digite o novo telefone: ");
                    break;

                case "4":
                    cliente.Pontos = LerInteiro("Digite a nova quantidade de pontos: ");
                    break;

                case "5":
                    int resultado = Cliente.AtualizarCliente(cliente);

                    if (resultado > 0)
                        Console.WriteLine("\nCliente atualizado com sucesso!");
                    else
                        Console.WriteLine("\nNão foi possível atualizar o cliente.");

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

    private static void ExcluirCliente()
    {
        Console.WriteLine("-=-=-=-=-=-=- EXCLUIR CLIENTE -=-=-=-=-=-=-\n");

        Cliente? cliente = BuscarPorIdValidado();

        if (cliente == null)
        {
            VoltarParaMenu();
            return;
        }

        ExibirCliente(cliente);

        Console.Write("\nDeseja realmente excluir este cliente? (S/N): ");
        string confirmacao = (Console.ReadLine() ?? "").Trim();

        if (confirmacao.Equals("S", StringComparison.OrdinalIgnoreCase))
        {
            int resultado = Cliente.Deletar(cliente);

            if (resultado > 0)
                Console.WriteLine("\nCliente excluído com sucesso!");
            else
                Console.WriteLine("\nNão foi possível excluir o cliente.");
        }
        else
        {
            Console.WriteLine("\nExclusão cancelada.");
        }

        VoltarParaMenu();
    }

    // ===== VISUALIZAR PONTUAÇÃO =====

    private static void VisualizarPontuacao()
    {
        Console.WriteLine("-=-=-=-=-=-=- RANKING DE PONTUAÇÕES -=-=-=-=-=-=-\n");

        List<Cliente> lista = Cliente.ListarTodos();

        if (lista.Count == 0)
        {
            Console.WriteLine("Nenhum cliente cadastrado no sistema.");
            VoltarParaMenu();
            return;
        }

        List<Cliente> listaOrdenada = lista
            .OrderByDescending(c => c.Pontos)
            .ToList();

        foreach (Cliente cliente in listaOrdenada)
        {
            Console.WriteLine($"ID: {cliente.Id}");
            Console.WriteLine($"Nome: {cliente.Nome}");
            Console.WriteLine($"Pontos: {cliente.Pontos}");
            Console.WriteLine("-----------------------------\n");
        }

        VoltarParaMenu();
    }
}
