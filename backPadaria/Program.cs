using backPadaria.Models;
using backPadaria.Service;
using backPadaria.Services;
using System.Data;
using System.Diagnostics.Contracts;

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

            //List<Cliente> clientes = Cliente.ListarTodos();

            //foreach (Cliente cliente in clientes)
            //{
            //    Console.WriteLine($"ID {cliente.Id}");
            //    Console.WriteLine($"Nome {cliente.Nome}");
            //    Console.WriteLine($"CPF {cliente.Cpf}");
            //    Console.WriteLine($"Telefone {cliente.Telefone}");
            //    Console.WriteLine($"Endereço {cliente.Endereco}\n");
            //}


            //    Cliente? meuCliente = Cliente.ListarPorId(1);

            //    if (meuCliente != null)
            //    {
            //        meuCliente.Endereco = "Rua Barão de Alagoas, 171";
            //        meuCliente.Telefone = "1199995-4334";

            //        int resultado = Cliente.AtualizarCliente(meuCliente);

            //        if (resultado > 0)
            //        {
            //            Console.WriteLine("Cliente atualizado com sucesso!");
            //        }
            //        else
            //        {
            //            Console.WriteLine("Falha ao atualizar o cliente.");
            //        }
            //    }

            //        if (meuCliente != null)
            //        {
            //            Console.WriteLine($"ID {meuCliente.Id}");
            //            Console.WriteLine($"Nome {meuCliente.Nome}");
            //            Console.WriteLine($"CPF {meuCliente.Cpf}");
            //            Console.WriteLine($"Telefone {meuCliente.Telefone}");
            //            Console.WriteLine($"Endereço {meuCliente.Endereco}\n");
            //        }
            //        else
            //        {
            //            Console.WriteLine("O cliente não foi encontrado!!");
            //        }
            //

            
            /*Funcionario funcionario1 = new Funcionario(
                nome: "Pedro",
                cpf: "123.456.789-01",
                cargo: "Empacotador"
            );

            // Funcionario.Inserir(funcionario1);

            /*var resultado = Funcionario.ListarPorId(5);

            Funcionario.Deletar(resultado);

            List<Funcionario> listaDeFuncionarios = new();
            listaDeFuncionarios = Funcionario.ListarTodos();

            foreach (Funcionario funcionario in listaDeFuncionarios)
            {
                Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
                Console.WriteLine($"ID {funcionario.Id}");
                Console.WriteLine($"Nome {funcionario.Nome}");
                Console.WriteLine($"CPF {funcionario.Cpf}");
                Console.WriteLine($"Cargo {funcionario.Cargo}\n");
                Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\n");
            }

            Funcionario? meuFuncionario = Funcionario.ListarPorId(3);

            if (meuFuncionario != null)
            {
                meuFuncionario.Nome = "Apenas Pedro";
                meuFuncionario.Cargo = "Emapacotador";

                int resultado = Funcionario.AtualizarFuncionario(meuFuncionario);

                if (resultado > 0)
                {
                    Console.WriteLine("Cliente atualizado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Falha ao atualizar o cliente.");
                }
            }

            if (meuFuncionario != null)
            {
                Console.WriteLine($"ID {meuFuncionario.Id}");
                Console.WriteLine($"Nome {meuFuncionario.Nome}");
                Console.WriteLine($"CPF {meuFuncionario.Cpf}");
                Console.WriteLine($"Cargo {meuFuncionario.Cargo}");
            }
            else
            {
                Console.WriteLine("O funcionario não foi encontrado!!")
            };*/

            //ClienteService.MenuCliente();
            FuncionarioService.MenuFuncionario();
        }
    }
}

