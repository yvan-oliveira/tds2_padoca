using backPadaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Services;

internal class ClienteService
{
    public static void MenuCliente()
    {
        bool sair = false;

        while (!sair)
        {
            Console.WriteLine("===== GESTÃO DE CLIENTES ==== ");
            Console.WriteLine("1 - Cadastrar Clientes");
            Console.WriteLine("2 - Listar Todos os Clientes");
            Console.WriteLine("3 - Buscar Cliente por ID");
            Console.WriteLine("4 - Atualizar Cliente");
            Console.WriteLine("5 - Excluir Cliente");
            Console.WriteLine("6 - Visualizar pontuaçao");
            Console.WriteLine("0 - Sair do Programa");

            Console.WriteLine("======================\n");

            Console.WriteLine("Digite uma opção: ");
            string opcao = Console.ReadLine();
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
                        Console.Clear();
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        Thread.Sleep(4000);
                        Console.Clear();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }
        }
    }

    private static void voltarParaMenu()
    {
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
        Console.Clear();
    }

    private static void CadastrarCliente()
    {
        Console.WriteLine("-=-=-=-=-=-=- CADASTRAR CLIENTES -=-=-=-=-=-=-");

        Console.WriteLine("Digite o nome do cliente: ");
        string nomeCliente = Console.ReadLine()!;

        Console.WriteLine("Digite o endereço do cliente: ");
        string enderecoCliente = Console.ReadLine()!;

        Console.WriteLine("Digite o cpf do cliente: ");
        string cpfCliente = Console.ReadLine()!;

        Console.WriteLine("Digite o telefone do cliente: ");
        string telefoneCliente = Console.ReadLine()!;

        Console.WriteLine("Digite a quantidade de pontos: ");
        int qtdPontos = int.Parse(Console.ReadLine()!);


        Cliente novoCliente = new Cliente(
            nome: nomeCliente,
            cpf: cpfCliente,
            telefone: telefoneCliente,
            endereco: enderecoCliente,
            pontos: qtdPontos
        );

        int resultadoInsert = Cliente.Inserir(novoCliente);

        if (resultadoInsert > 0) Console.WriteLine("Cliente cadastrado com sucesso!");

        voltarParaMenu();
    }

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
                Console.WriteLine($"ID: {cliente.Id}");
                Console.WriteLine($"Nome: {cliente.Nome}");
                Console.WriteLine($"CPF: {cliente.Cpf}");
                Console.WriteLine($"Telefone: {cliente.Telefone}");
                Console.WriteLine($"Endereço: {cliente.Endereco}");
                Console.WriteLine("-----------------------------\n");
            }
        }
        voltarParaMenu();
    }

    private static void BuscarClientePorId()
    {
        Console.WriteLine("-=-=-=-=-=-=- BUSCAR POR ID -=-=-=-=-=-=-\n");

        Console.WriteLine("Digite o id: ");
        int id = int.Parse(Console.ReadLine()!);

        if (id <= 0)
        { 
            Console.WriteLine("\nID inválido. O ID deve ser um número positivo.");
            voltarParaMenu();
            return;
        }

        Cliente clienteEncontrado = Cliente.ListarPorId(id);


        if (clienteEncontrado != null)
        {
            Console.WriteLine($"\nID: {clienteEncontrado.Id}");
            Console.WriteLine($"Nome: {clienteEncontrado.Nome}");
            Console.WriteLine($"CPF: {clienteEncontrado.Cpf}");
            Console.WriteLine($"Telefone: {clienteEncontrado.Telefone}");
            Console.WriteLine($"Endereço: {clienteEncontrado.Endereco}");
            Console.WriteLine($"Pontos: {clienteEncontrado.Pontos}");
            Console.WriteLine("-----------------------------\n");
        }
        else
        {
            Console.WriteLine($"\nCliente com ID {id} não encontrado.\n");
        }
        voltarParaMenu();
    }

    private static void AtualizarCliente()
    {
        Console.WriteLine("-=-=-=-=-=-=- Atualizar Cliente -=-=-=-=-=-=-\n");

        Console.Write("Digite o id: ");
        int id = int.Parse(Console.ReadLine()!);

        if (id <= 0) 
        {
            Console.WriteLine("\nID inválido. O ID deve ser um número positivo.");
            voltarParaMenu();
            return;
        }

        Cliente clienteEncontrado = Cliente.ListarPorId(id);

        if (clienteEncontrado != null)
        {
            string opcaoEscolhida = "";

            while (opcaoEscolhida != "5")
            {
                Console.Clear();
                Console.WriteLine("-=-=-=-=-=-=- Atualizar Cliente -=-=-=-=-=-=-\n");

                Console.WriteLine("Estado Atual: \n");

                Console.WriteLine($"ID: {clienteEncontrado.Id}");
                Console.WriteLine($"Nome: {clienteEncontrado.Nome}");
                Console.WriteLine($"CPF: {clienteEncontrado.Cpf}");
                Console.WriteLine($"Telefone: {clienteEncontrado.Telefone}");
                Console.WriteLine($"Endereço: {clienteEncontrado.Endereco}");
                Console.WriteLine($"Pontos: {clienteEncontrado.Pontos}");
                Console.WriteLine("-----------------------------\n");

                Console.WriteLine("1 - Alterar o nome");
                Console.WriteLine("2 - Alterar o endereço");
                Console.WriteLine("3 - Alterar o telefone");
                Console.WriteLine("4 - Alterar a quantidade de pontos");
                Console.WriteLine("5 - Confirmar alterações\n");

                Console.Write("Digite uma opção: ");
                opcaoEscolhida = Console.ReadLine()!;

                switch (opcaoEscolhida)
                {
                    case "1":
                        Console.WriteLine("\nDigite o novo nome do cliente: ");
                        clienteEncontrado.Nome = Console.ReadLine()!;
                        break;
                    case "2":
                        Console.WriteLine("\nDigite o novo endereço do cliente: ");
                        clienteEncontrado.Endereco = Console.ReadLine()!;
                        break;
                    case "3":
                        Console.WriteLine("\nDigite o novo telefone do cliente: ");
                        clienteEncontrado.Telefone = Console.ReadLine()!;
                        break;
                    case "4":
                        Console.WriteLine("\nDigite a nova quantidade de pontos: ");
                        clienteEncontrado.Pontos = int.Parse(Console.ReadLine()!);
                        break;
                    case "5":
                        int resultadoUpdate = Cliente.AtualizarCliente(clienteEncontrado);
                        
                        if (resultadoUpdate > 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Cliente atualizado com sucesso!");
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Erro ao atualizar o cliente.");
                        }
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida. Nenhuma alteração foi feita.");
                        Thread.Sleep(3000);
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine($"\nCliente com ID {id} não encontrado.\n");
        }

        Thread.Sleep(3000);
        Console.Clear();
    }

    private static void ExcluirCliente()
    {
        Console.WriteLine("-=-=-=-=-=-=- Excluir Cliente -=-=-=-=-=-=-\n");

        Console.Write("Digite o id: ");
        int id = int.Parse(Console.ReadLine()!);

        if (id <= 0)
        {
            Console.WriteLine("\nID inválido. O ID deve ser um número positivo.");
            voltarParaMenu();
            return;
        }

        Cliente clienteEncontrado = Cliente.ListarPorId(id);

        if (clienteEncontrado != null)
        {
            Console.WriteLine($"\nID: {clienteEncontrado.Id}");
            Console.WriteLine($"Nome: {clienteEncontrado.Nome}");
            Console.WriteLine($"CPF: {clienteEncontrado.Cpf}");
            Console.WriteLine($"Telefone: {clienteEncontrado.Telefone}");
            Console.WriteLine($"Endereço: {clienteEncontrado.Endereco}");
            Console.WriteLine($"Pontos: {clienteEncontrado.Pontos}");
            Console.WriteLine("-----------------------------\n");

            Console.WriteLine("Deseja realmente excluir este cliente? (S/N)");
            string confirmacao = Console.ReadLine()!.ToUpper();

            if (confirmacao == "S")
            {
                int resultadoDelete = Cliente.Deletar(clienteEncontrado);

                if (resultadoDelete > 0)
                {
                    Console.WriteLine("\nCliente excluído com sucesso!");
                }
                else
                {
                    Console.WriteLine("\nErro ao excluir o cliente.");
                }
            }
            else
            {
                Console.WriteLine("\nExclusão cancelada.");
            }
        }

        voltarParaMenu();
    }

    private static void VisualizarPontuacao()
    {
        Console.Clear();
        Console.WriteLine("-=-=-=-=-=-=- VISUALIZAR PONTUAÇÕES -=-=-=-=-=-=-\n");

        List<Cliente> lista = Cliente.ListarTodos();

        if (lista.Count == 0)
        {
            Console.WriteLine("Nenhum cliente cadastrado no sistema.");
        }

        List<Cliente> listaOrdenada = lista.OrderByDescending(c => c.Pontos).ToList();

        foreach (Cliente cliente in listaOrdenada)
        {
            Console.WriteLine($"ID: {cliente.Id}");
            Console.WriteLine($"Nome: {cliente.Nome}");
            Console.WriteLine($"Pontos: {cliente.Pontos}");
            Console.WriteLine("-----------------------------\n");
        }

        voltarParaMenu();
    }
}
