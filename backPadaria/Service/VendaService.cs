using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Service;

public static class VendaService
{
    public static void MenuVenda()
    {

        bool sair = false;

        while (!sair)

        {

            Console.Clear();

            Console.WriteLine("-=-=-=-= PDV - Padaria Pão & Cia =-=-=-=-");
            Console.WriteLine("1 - Inciar nova Venda");
            Console.WriteLine("0 - Voltar ao Menu Principal");
            Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");

            Console.Write("\nEscolha uma opção: ");
            string opcao = Console.ReadLine();

            try
            {
                switch (opcao)
                {
                    case "1":
                        IniciarVenda();
                        break;
                    case "0":
                        sair = true;
                        break;
                    default:
                        Console.WriteLine("Opção Inválida");
                        PressionarParaVoltar();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na venda {ex.Message}");
                PressionarParaVoltar();
            }
        }
    }

    public static void IniciarVenda()
    {
        Console.WriteLine("deu certo");
        PressionarParaVoltar();
    }

    public static void PressionarParaVoltar()
    {
        Console.WriteLine("\nOpção inválida. Pressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
}
