using backPadaria.Models;
using System.Data;

namespace backPadaria
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* Banco.Banco.connectAndTest();

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


            Cliente cliente1 = new Cliente(
                nome: nomeCliente,
                cpf: cpfCliente,
                telefone: telefoneCliente,
                endereco: enderecoCliente,
                pontos: qtdPontos
            );
            Cliente.Inserir(cliente1);
            */

            List<Cliente> clientes = Cliente.ListarTodos();

            foreach (Cliente cliente in clientes)
            {
                Console.WriteLine($"ID {cliente.Id}");
                Console.WriteLine($"Nome {cliente.Nome}");
                Console.WriteLine($"CPF {cliente.Cpf}");
                Console.WriteLine($"Telefone {cliente.Telefone}");
                Console.WriteLine($"Endereço {cliente.Endereco}\n");
            }

        }
    }
}
