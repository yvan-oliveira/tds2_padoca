using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using backPadaria.banco;
using System.Threading.Tasks;

namespace backPadaria.Models;

internal class  Funcionario
{
    private int _id;
    private string _nome;
    private string _cpf;
    private string _cargo;

    public int Id { get => _id; set => _id = value; }
    public string Nome { get => _nome; set => _nome = value; }
    public string Cpf { get => _cpf; set => _cpf = value; }
    public string Cargo { get => _cargo; set => _cargo = value; }

    public Funcionario(string nome, string cpf, string cargo)
    {
        this._nome = nome;
        this._cpf = cpf;
        this._cargo = cargo;
    }

    public Funcionario(int id, string nome, string cpf, string cargo)
    {
        this._id = id;
        this._nome = nome;
        this._cpf = cpf;
        this._cargo = cargo;
    }

    public static int Inserir(Funcionario funcionario)
    {
        string sql = "INSERT INTO Funcionario (`Nome`, `CPF`, `Cargo`) VALUES (@Nome, @CPF, @Cargo)";

        using(var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
            cmd.Parameters.AddWithValue("@CPF", funcionario.Cpf);
            cmd.Parameters.AddWithValue("@Cargo", funcionario.Cargo);
            return Banco.ExecutarComando(cmd);
        }
    }

    public static int Deletar(Funcionario funcionario)
    {
        string sql = "DELETE FROM `Funcionario` WHERE `Id` = @id";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@id", funcionario.Id);

            return Banco.ExecutarComando(cmd);
        }
    }

    public static List<Funcionario> ListarTodos()
    {
        string sql = "SELECT * FROM `Funcionario`";

        List<Funcionario> listarFuncionarios = new();

        using (var cmd = new MySqlCommand(sql))
        {
            // Executa a leitura. 0 bloco 'using' aqui é FUNDAMENTAL para garantir que o
            // reader seja fechado no final, o que por sua vez fecha a conexao com o banco.
            using (MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd))
            {
                // O while vai rodar uma vez para CADA linha que existir na tabela Cliente
                while (reader.Read())
                {
                    int id = reader.GetInt32("Id");
                    string nome = ValidarCampo(reader["Nome"]);
                    string cpf = ValidarCampo(reader["CPF"]);
                    string cargo = ValidarCampo(reader["Cargo"]);

                    // Cria o objeto Cliente com os dados extraídos
                    Funcionario funcionario = new Funcionario(id, nome, cpf, cargo);

                    // Adiciona este cliente à nossa lista
                    listarFuncionarios.Add(funcionario);
                }
            }

            // Retorna a lista completa (se a tabela estiver vazia, retorna uma lista com 0 itens)
            return listarFuncionarios;
        }
    }

    public static Funcionario? ListarPorId(int id)
    {
        string sql = "SELECT * FROM `Funcionario` WHERE `id` = @Id";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@Id", id);

            MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd);

            Funcionario? funcionario = null;

            while (reader.Read())
            {
                int idBanco = reader.GetInt32("Id");
                string nome = ValidarCampo(reader["Nome"]);
                string cpf = ValidarCampo(reader["CPF"]);
                string cargo = ValidarCampo(reader["Cargo"]);

                // Cria o objeto Cliente com os dados extraídos
                funcionario = new Funcionario(id, nome, cpf, cargo);
            }

            return funcionario;
        }
    }

    public static int AtualizarFuncionario(Funcionario funcionario)
    {
        string sql = "UPDATE `Funcionario` SET `Nome` = @Nome, `Cpf` = @CPF, `Cargo` = @Cargo WHERE `Id` = @id";

        using (var cmd = new MySqlCommand(sql))
        {
            // Vinculação de Parâmetros: Protege contra SQL Injection e organiza os dados
            cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
            cmd.Parameters.AddWithValue("@CPF", funcionario.Cpf);
            cmd.Parameters.AddWithValue("@Cargo", funcionario.Cargo);

            // A chave de busca: Quem será atualizado?
            cmd.Parameters.AddWithValue("@id", funcionario.Id);

            // Execução: O UPDATE também é uma instrução de ação (como INSERT e DELETE).
            // Ele vai retornar 1 (se encontrou  o ID e atualizou com sucesso) ou 0 (se o ID não existia).
            return Banco.ExecutarComando(cmd);
        }
    }

    private static string ValidarCampo(object campo)
    {
        if (campo == DBNull.Value || campo == null)
        {
            return "";
        }
        else
        {
            return campo.ToString();
        }
    }
}
