using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backPadaria.Banco
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

                    //teste

                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"ERRO ao conctar no banco de dados");
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
