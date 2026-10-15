using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.banco
{
    public static class Banco
    {
        private static string stringDeConexao = "Server=10.91.45.58;Port=3306;Database=padaria_bd;Uid=admin;Pwd=12345678";

        public static void connectAndTest()
        {
            using (var connection = new MySqlConnection(stringDeConexao))
            {
                try
                {
                    Console.WriteLine("Tentando conectar ao banco de dados");
                    connection.Open();
                    Console.WriteLine($"Conexão bem sucedida");

                    using (var command = new MySqlCommand("SELECT VERSION();", connection)) {
                        string? version = command.ExecuteScalar()?.ToString();
                        Console.WriteLine($"Versão do MariaDB {version}");
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"ERRO ao conctar no banco de dados");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        // Delcaração do método: público e estático, retorna um número  inteiro (linhas afetadas)
        // e recebe um objeto MySqlCommand com a instrução SQL já confirmada como parâmetro.
        public static int ExecutarComando(MySqlCommand command)
        {
            /*O bloco 'using' garante que a conexão será automaticamente fechada e liberada 
                da memória ao final do bloco, mesmo que ocorram erros no meio do processo.

            Cria a conexão utilizando a variavel de ambiente.*/
            using (var conexao = new MySqlConnection(stringDeConexao))
            {
                // Vinculação: Diz ao comando SQL em qual conexão ele deve ser executado.
                command.Connection = conexao;

                // Tratamento de erros: Inicia um bloco de tentativa para proteger o programa de falhas abruptas.
                try
                {
                    // Abertura da conexão: Estabelece efetivamente a comunicação com o servidor MySQL.
                    conexao.Open();

                    // Execução: O ExecuteNonQuery dispara comandos de ação (INSERT, UPDATE, DELETE).
                    // Ele não retorna registros, mas sim o número inteiro de linhas que forma alteradas,
                    // e o 'return' devolve esse número para quem chamou o método.

                    return command.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"Erro e Banco de dados {ex.Message}");

                    return -1;
                }
            }
        }
    }
}
