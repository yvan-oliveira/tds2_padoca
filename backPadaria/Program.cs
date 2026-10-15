using backPadaria.Models;

namespace backPadaria
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Banco.Banco.connectAndTest();

            Cliente cliente1 = new Cliente(
                nome: "Raphael Oliveira2",
                cpf: "12122233345",
                telefone: "111111111",
                endereco: "Rua do SENAC",
                pontos: 10
            );

            Cliente.Inserir(cliente1);
        }
    }
}
