using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

                    using (var command = new MySqlCommand("SELECT VERSION();", connection))
                    {
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

        public static int ExecutarComandoComRetornoId(MySqlCommand command)
        {
            using (var conexao = new MySqlConnection(stringDeConexao))
            {
                command.Connection = conexao;

                try
                {
                    conexao.Open();
                    /*  
                        O ExecuteScalar executa o comando e retorna APENAS a primeira coluna da primeira
                        linha do resultado. É perfeito para pegar um único valor (como um ID recém-gerado).
                        Ele retorna um tipo 'object' porque não sabe de antemão qual o tipo do dado no banco.
                    */

                    object objRetorno = command.ExecuteScalar();

                    int retorno = 0;
                    /*  
                        Verificação de segurança: Checa se o banco realmente retornou algo.
                        Verifica tanto se o objeto C# não é nulo (null) quanto se
                        não é um nulo do próprio banco de dados(DBNull.Value).
                    */
                    if (objRetorno != null && objRetorno != DBNull.Value)
                    {
                        /*  
                            Se passou na verificação, converte o objeto (que veio como texto/objeto)
                            para um número inteiro (int).
                        */

                        retorno = int.Parse(objRetorno.ToString());
                    }
                    return retorno;
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"Erro e Banco de dados {ex.Message}");
                    return -1;
                } // Metodo RetornoComId

            }

        }

        // Metodo central para executar comandos de consulta (SELECT) no banco de dados.
        // Ele retorna um MySqlDataReader, que é uma espécie de "ponteiro" para ler os dados linha por linha.
        public static MySqlDataReader ExecutarComRetornoDeDados(MySqlCommand command)
        {
            // Cria a conexão com o banco.
            // ATENÇÃO: Aqui NÃO usamos o bloco 'using' para a conexão. Se usássemos, a conexão
            // seria fechada antes de retornar os dados, e o programa quebraria ao tentar ler a tabela.
            var conexao = new MySqlConnection(stringDeConexao);


            try
            {
                conexao.Open();

                command.Connection = conexao;
                // Executa a consulta e retorna o "leitor" de dados.
                // O CommandBehavior.CloseConnection e a peça-chave deste método: ele avisa ao leitor
                // que assim que ele (o leitor) for fechado ou descartado la na tela/classe que o chamou,
                // a conexao com o banco deve ser fechada automaticamente junto com ele.
                return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
            catch (MySqlException ex)
            {
                // O Disose() já executa o Close() e libera a mémoria.
                conexao.Dispose();

                Console.WriteLine($"Erro na leitura do banco de dados: {ex.Message}");

                // Ao invés de retornar null, relança o erro (throw) para que o método que
                // chamou este ExecuteReader saiba que a pesquisa falhou e possa tratar a tela do usuário.
                throw;
            }
        }
    }
}
