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
        Console.WriteLine("1 - Cadastrar Cliente");

        bool sair = false;

        while (!sair)
        {
            Console.WriteLine(" ===== GESTÃO DE CLIENTES ==== ");
            Console.WriteLine("2 - Listar Todos os Clientes");
            Console.WriteLine("3 - Buscar Cliente por ID");
            Console.WriteLine("4 - Atualizar Cliente");
            Console.WriteLine("5 - Excluir Cliente");
            Console.WriteLine("6 - Visualizar pontuaçao");
            Console.WriteLine("0 - Sair do Programa");

            Console.WriteLine("======================");

            string opcao = Console.ReadLine();
            Console.Clear();
        }
    }
}
