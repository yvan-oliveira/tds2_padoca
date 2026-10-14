using backPadaria.Models;

namespace backPadaria
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Banco.Banco.connectAndTest();

            Cliente cliente1 = new Cliente(
                nome: " ",
                cpf: "11122233345",
                telefone: "111111111",
                endereco: "Rua tirusbango tirusbagos",
                pontos: 123
            );
        }
    }
}
